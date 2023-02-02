using FluentValidation;
using Jwt.Applications.User;
using Jwt.Infrastructure.Database;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Jwt.Applications
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            //logic here
            services.AddSingleton<IUserCommonProcess, UserCommonProcess>();
            services.AddSingleton<IQuery, SqlServer>();
            return services;
        }
    }
}