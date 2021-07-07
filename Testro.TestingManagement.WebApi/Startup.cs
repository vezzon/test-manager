using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Testro.TestingManagement.WebApi.Configurations;
using Testro.TestingManagement.WebApi.DataAccess;
using Testro.TestingManagement.WebApi.Middleware;
using Testro.TestingManagement.WebApi.Models;
using Testro.TestingManagement.WebApi.Repositories;
using Testro.TestingManagement.WebApi.Services;
using Testro.TestingManagement.WebApi.Workers;

namespace Testro.TestingManagement.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddCors(opt => opt.AddDefaultPolicy(builder => 
                builder.AllowAnyOrigin().AllowAnyHeader()));

            services.Configure<JwtConfig>(Configuration.GetSection("JwtConfig"));

            services.AddAutoMapper(typeof(Startup));

            
            services.AddMvc(o => o.EnableEndpointRouting = false)
                .SetCompatibilityVersion(CompatibilityVersion.Latest)
                .AddNewtonsoftJson(o =>
                    o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            services
                .AddScoped<EntityService<TestCase>>()
                .AddScoped<EntityService<TestScenario>>()
                .AddScoped<EntityService<TestProject>>()
                .AddScoped<IRepository<TestCase>, Repository<TestCase>>()
                .AddScoped<IRepository<TestScenario>, Repository<TestScenario>>()
                .AddScoped<IRepository<TestProject>, Repository<TestProject>>();

            services.AddHostedService<TimeWorker>();

            services.AddScoped<ErrorHandlingMiddleware>();
            
            services.AddDbContext<DatabaseContext>(o =>
                o
                    .UseLazyLoadingProxies()
                    .UseMySql(
                    Configuration.GetConnectionString("DefaultConnection"),
                    new MariaDbServerVersion(new Version(10,5)), 
                    mySqlOptionsAction => mySqlOptionsAction
                        .CharSetBehavior(CharSetBehavior.NeverAppend)
                    )
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors()
            );

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
                .AddJwtBearer(jwt =>
                {
                    var key = Encoding.ASCII.GetBytes(Configuration["JwtConfig:Secret"]);
                    jwt.SaveToken = true;
                    jwt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        RequireExpirationTime = false
                    };
                });

            services.AddDefaultIdentity<IdentityUser>(opt => opt.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<DatabaseContext>();
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Testro.TestingManagement.WebApi", Version = "v1"});
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference 
                            { 
                                Type = ReferenceType.SecurityScheme, 
                                Id = "Bearer" 
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Testro.TestingManagement.WebApi v1"));
            }

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();
            
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}