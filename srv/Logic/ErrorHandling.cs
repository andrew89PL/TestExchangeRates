namespace TestExchangeRates.Logic
{
    public class ErrorHandling : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
			try
			{
				await next.Invoke(context);
			}
			catch (Exception e)
			{
				context.Response.StatusCode = 200;
				await context.Response.WriteAsync(e.Message);
			}
        }
    }
}
