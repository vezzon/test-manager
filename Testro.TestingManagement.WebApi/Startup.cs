using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Testro.TestingManagement.WebApi.DataAccess;
using Testro.TestingManagement.WebApi.Middleware;
using Testro.TestingManagement.WebApi.Models;
using Testro.TestingManagement.WebApi.Repositories;
using Testro.TestingManagement.WebApi.Services;

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

            services.AddAutoMapper(typeof(Startup));

            services.AddHttpClient<NBPGoldService>();
            
            services.AddMvc(o => o.EnableEndpointRouting = false)
                .SetCompatibilityVersion(CompatibilityVersion.Latest)
                .AddNewtonsoftJson(o =>
                    o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            services
                .AddScoped<EntityService<TestCase>>()
                .AddScoped<EntityService<TestScenario>>()
                .AddScoped<EntityService<TestProject>>()
                .AddScoped<Repository<TestCase>>()
                .AddScoped<Repository<TestScenario>>()
                .AddScoped<Repository<TestProject>>()
                .AddScoped<NBPGoldService>();

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
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Testro.TestingManagement.WebApi", Version = "v1"});
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}