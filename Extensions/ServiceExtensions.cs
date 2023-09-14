using HackerNews.LoggerService;
using Microsoft.Extensions.DependencyInjection;

namespace HackerNews.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }
    }
}
