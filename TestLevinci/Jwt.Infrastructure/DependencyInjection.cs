using Jwt.Infrastructure.Configuations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Jwt.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton(resolver => resolver.GetRequiredService<IOptionsMonitor<AppSettings>>().CurrentValue);

            services.Configure<AppSettings>(Configuration.GetSection(nameof(AppSettings)));
            services.AddHttpClient();


            return services;
        }

        public static IApplicationBuilder AddInfrastructure(this IApplicationBuilder app)
        {
            AppSettingServices.Services = app.ApplicationServices;

            return app;
        }
    }
}