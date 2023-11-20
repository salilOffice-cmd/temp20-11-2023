namespace APIarchitecturePractice.CustomMiddlewares
{

    // This middleware shows the necessary steps for creating a middleware


    public class firstCustomMiddleware
    {

        // The RequestDelegate is a delegate representing the next middleware
        // component in the pipeline.
        // It allows you to invoke the next middleware after your custom logic is executed.
        private readonly RequestDelegate _next;

        public firstCustomMiddleware(RequestDelegate next)
        {
            _next = next;
        }


        // This is the main method of your middleware class.
        // It's named InvokeAsync, as this is the method that will be called
        // when the middleware is invoked.
        // 'HttpContext' object has all the information about request and response
        public async Task InvokeAsync(HttpContext context)
        {
            // Your middleware logic goes here
            Console.WriteLine($"Request Method: {context.Request.Method}, Route: {context.Request.Path}");


            // Call the next middleware in the pipeline
            await _next(context);


            // Last step is to add this middleware to the application
            // see (./Extensions/)
        }
    }
}
