using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Testro.TestingManagement.WebApi.Exceptions;

namespace Testro.TestingManagement.WebApi.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        // TODO Add logger
        
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (NotFoundException e)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(e.Message);
            }
            catch (Exception e)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync(e.Message);
            }
        }
    }
}