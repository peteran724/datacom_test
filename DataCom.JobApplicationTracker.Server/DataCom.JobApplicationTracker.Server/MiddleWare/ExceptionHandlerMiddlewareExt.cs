namespace DataCom.JobApplicationTracker.Server.MiddleWare
{
    public static class ExceptionHandlerMiddlewareExt
    {
        public static IApplicationBuilder UseExHandlerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlerMiddleware>();
        }

    }
}
