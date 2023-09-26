namespace mvcproject.Authservis
{
    public class DelayMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly TimeSpan _delay;

        public DelayMiddleware(RequestDelegate next, TimeSpan delay)
        {
            _next = next;
            _delay = delay;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Method == "GET")
            {
                await Task.Delay(_delay);
            }

            await _next(context);
        }
    }
}
