using GameMasterEnterprise.Data.Context;
using GameMasterEnterprise.Data.Repository;
using GameMasterEnterprise.Domain.Intefaces;
using GameMasterEnterprise.Domain.Notificacoes;
using GameMasterEnterprise.Service.Services;
using Ipet.API.Extensions;
using Microsoft.AspNetCore.Mvc.DataAnnotations;

namespace GameMasterEnterprise.API.Configuration
{
    public static class DependenceInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<INotificador, Notificador>();
            services.AddScoped<IUser, AspNetUser>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<MeuDbContext>();

            services.AddScoped<ICassinoService, CassinoService>();
            services.AddScoped<ICassinoRepository, CassinoRepository>();


            services.AddScoped<INotificador, Notificador>();
            return services;
        }
    }
}
