using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using WebApplication7.Entities.Responses;
using WebApplication7.Exceptions;

namespace WebApplication7.Middlewares
{
    public class ErrorHandlerMiddleware
    {

        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                switch (error)
                {
                    case BadRequestException e:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case KeyNotFoundException e:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                string message = error.InnerException != null ? error.InnerException.Message : error?.Message;

                string result = JsonConvert.SerializeObject(new ErrorMessageResponse
                {
                    Errors = new Dictionary<string, string[]>{
                        {"Message", new string[1] { message } }
                    },
                    Status = response.StatusCode,
                    TraceId = context.TraceIdentifier
                });
                
                await response.WriteAsync(result);
            }
        }

    }
}
