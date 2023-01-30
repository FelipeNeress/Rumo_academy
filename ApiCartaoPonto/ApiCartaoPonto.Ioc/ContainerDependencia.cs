using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiCartaoPonto.Repositories.Repositorio;
using ApiCartaoPonto.Services;
using FluentAssertions.Common;
using Microsoft.Extensions.DependencyInjection;

namespace ApiCartaoPonto.Ioc
{
    public class ContainerDependencia
    {
        public static void RegistrarServicos(IServiceCollection services)
        {
            //repositorios
            services.AddScoped<FuncionarioRepositorio, FuncionarioRepositorio>();
            services.AddScoped<CargoRepositorio, CargoRepositorio>();
            services.AddScoped<LiderancaRepositorio, LiderancaRepositorio>();
            services.AddScoped<EquipeRepositorio, EquipeRepositorio>();
            services.AddScoped<PontoRepositorio, PontoRepositorio>();

            //services
            services.AddScoped<FuncionarioService, FuncionarioService>();
            services.AddScoped<CargoService, CargoService>();
            services.AddScoped<LiderancaService, LiderancaService>();
            services.AddScoped<EquipeService, EquipeService>();
            services.AddScoped<PontoServices, PontoServices>();
        }
    }
}
