using GameMasterEnterprise.Data.Context;
using GameMasterEnterprise.Data.Repository;
using GameMasterEnterprise.Domain.Intefaces;
using GameMasterEnterprise.Domain.Notificacoes;
using GameMasterEnterprise.Service.Services;
using Ipet.API.Extensions;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using System.Net.Http;

namespace GameMasterEnterprise.API.Configuration
{
    public static class DependenceInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<INotificador, Notificador>();
            services.AddScoped<HttpClient, HttpClient>();
            services.AddScoped<IUser, AspNetUser>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<MeuDbContext>();

            services.AddScoped<ICassinoService, CassinoService>();
            services.AddScoped<ICassinoRepository, CassinoRepository>();

            services.AddScoped<IPlayerService, PlayerService>();
            services.AddScoped<IPlayerRepository, PlayerRepository>();

            services.AddScoped<IPlayerSaldoService, PlayerSaldoService>();
            services.AddScoped<IPlayerSaldoRepository, PlayerSaldoRepository>();

            services.AddScoped<IJogoService, JogoService>();
            services.AddScoped<IJogoRepository, JogoRepository>();

            services.AddScoped<ISessaoService, SessaoService>();
            services.AddScoped<ISessaoRepository, SessaoRepository>();

            services.AddScoped<IMasterService, MasterService>();

            services.AddScoped<INotificador, Notificador>();
            return services;
        }
    }
}
