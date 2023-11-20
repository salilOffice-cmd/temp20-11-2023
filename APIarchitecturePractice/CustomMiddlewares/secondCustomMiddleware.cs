namespace APIarchitecturePractice.CustomMiddlewares
{
    public class secondCustomMiddleware
    {
        private readonly RequestDelegate _next;

        public secondCustomMiddleware(RequestDelegate next)
        {
            _next = next;
        }


        public async Task InvokeAsync(HttpContext context)
        {
            // Your middleware logic goes here
            Console.WriteLine($"Request2 Method2: {context.Request.Method}, Route2: {context.Request.Path}");


            // Call the next middleware in the pipeline
            try
            {
                //throw new Exception("Exception from second middleware");
                await _next(context);
            }

            catch (Exception ex)
            {
                await context.Response.WriteAsync(ex.Message);
            }

        }
    }
}
