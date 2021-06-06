using Catalog.Application.Common.exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Catalog.WebApi.Filters
{
    public class ApiExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ApiExceptionHandlingMiddleware> _logger;

        public ApiExceptionHandlingMiddleware(
            RequestDelegate next,
            ILogger<ApiExceptionHandlingMiddleware> logger
        )
        {
            _next = next;
            _logger = logger;
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

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            string result;

            if (ex is CatalogApiExceptions mdpApiException)
            {

                var details = new
                {
                    Code = StatusCodes.Status422UnprocessableEntity,
                    Message = mdpApiException.Message
                };

                result = System.Text.Json.JsonSerializer.Serialize(details);
                context.Response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
            }
            else if (ex is CatalogValidationException validationException)
            {
                var details = new
                {
                    Code = StatusCodes.Status400BadRequest,
                    Errors = validationException.Errors.ToDictionary(x => x.Key, x => x.Value),
                    Message = "Validation Error"
                };
                result = System.Text.Json.JsonSerializer.Serialize(details);

            }
            else
            {
                _logger.LogError(ex, "Unhandled exception has occurred. with message:{Message}, with trace: {trace}", ex.Message, ex.StackTrace);
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                result = JsonSerializer.Serialize(new { errors = "Internal Server Error!" });
            }

            context.Response.ContentType = "application/json";

            return context.Response.WriteAsync(result);
        }
    }

}
