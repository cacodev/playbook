using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Playbook
{
    public static class PlaybookStartupExtensions
    {
        public static IServiceCollection AddPlaybook(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<PlaybookConfig>(config);
            services.AddScoped<IPlaybookService, PlaybookService>();
            return services;
        }
        public static IApplicationBuilder UsePlaybook(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<PlaybookMiddleware>();
            return builder;
        }
    }
}