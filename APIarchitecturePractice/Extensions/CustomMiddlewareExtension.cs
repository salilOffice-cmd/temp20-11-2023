using APIarchitecturePractice.CustomMiddlewares;

namespace APIarchitecturePractice.Extensions
{
    public static class CustomMiddlewareExtension
    {
        public static void ConfigureFirstCustomMiddleware(this WebApplication app)
        {
            //app.UseMiddleware<secondCustomMiddleware>();
            //app.UseMiddleware<firstCustomMiddleware>();
            //app.UseMiddleware<thirdCustomMiddleware>();
            app.UseMiddleware<GlobalExceptionHandler>();

            // Now after this code
            // Use this extension method in Program.cs like below
            // app.ConfigureFirstCustomMiddleware()
        }
    }
}
