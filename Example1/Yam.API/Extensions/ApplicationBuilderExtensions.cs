using Yam.API.Infrastructure;

namespace Yam.API.Extensions
{
    internal static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseExceptionLogging(this IApplicationBuilder app)
        {
            _ = app ?? throw new ArgumentNullException(nameof(app));

            return app.UseMiddleware<ExceptionLoggingMiddleware>();
        }
    }
}
