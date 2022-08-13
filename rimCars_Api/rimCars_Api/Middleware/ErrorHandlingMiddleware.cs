using Microsoft.Extensions.Logging;
using rimCars_Api.Exceptations;

namespace rimCars_Api.Middleware
{
    public class ErrorHandlingMiddleware:IMiddleware
    {

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);

            }catch(BadRequestExceptation badRequestEx)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(badRequestEx.Message);
            }
            catch (Exception e)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Something went wrong");
            }
        }
    }
}
