using Domain.Exceptions;
using Domain.Exceptions.BadRequest;
using Domain.Exceptions.NotFound;
using Shared.ErrorModels;

namespace Store.Hossam.Middelwares
{
    public class GlobalErrorHandlingMiddelwares
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalErrorHandlingMiddelwares> _logger;

        public GlobalErrorHandlingMiddelwares(RequestDelegate next ,ILogger<GlobalErrorHandlingMiddelwares>logger)
        {
           
           _next = next;
           _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
                if (context.Response.StatusCode == StatusCodes.Status404NotFound) {


                //    context.Response.ContentType = "application/json";
                //    var response = new ErrorDetails()
                //    {
                //        StatusCode = StatusCodes.Status404NotFound,
                //        ErrorMessage = $"End Point {context.Request.Path} not found"

                //    };
                //    await context.Response.WriteAsJsonAsync(response);
                   throw new NotFoundException($"End Point11 {context.Request.Path} not found");


                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
               
                context.Response.ContentType = "application/json";
                var response = new ErrorDetails()
                {
                   
                    ErrorMessage = ex.Message

                };
                response.StatusCode = ex switch
                {
                    NotFoundException => StatusCodes.Status404NotFound,
                    BadRequestException => StatusCodes.Status400BadRequest,
                    UnAuthorizedException => StatusCodes.Status401Unauthorized,
                    ValdiationError => HandelValdiationExceptionAsync((ValdiationError)ex,response),
                    _ => StatusCodes.Status500InternalServerError
                };

                context.Response.StatusCode = response.StatusCode;
                await context.Response.WriteAsJsonAsync(response);
            }
        }

        private static int HandelValdiationExceptionAsync(ValdiationError ex , ErrorDetails responce)
        {
            responce.Errors = ex.Errors;
            return StatusCodes.Status400BadRequest;
        }

       
    }
}
