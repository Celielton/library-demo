using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Shared.Core.Exception;
using Shared.Core.Exception.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;

namespace Shared.API
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            HttpStatusCode code;
            var body = string.Empty;
            if (ex.GetType() == typeof(ValidationException) || ex.GetType() == typeof(IApplicationException) || ex.GetType() == typeof(NotFoundException))
            {
                code = HttpStatusCode.BadRequest;
                body = JsonConvert.SerializeObject(new { message = ex.InnerException != null ? ex.InnerException.Message : ex.Message });
            }
            else
            {
                code = HttpStatusCode.InternalServerError;
                body = JsonConvert.SerializeObject(new { message = ex.Message });
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            await context.Response.WriteAsync(body);
        }
    }

    public static class ExceptionHandlerMiddleware
    {
        public static void RegisterExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware(typeof(ExceptionMiddleware));
        }
    }
}
