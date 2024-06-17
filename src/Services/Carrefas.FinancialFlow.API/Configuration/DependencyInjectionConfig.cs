using Carrefas.Core.IoC;
using Carrefas.FinancialFlow.Application.Interfaces;
using Carrefas.FinancialFlow.Application.IoC;
using Carrefas.FinancialFlow.Application.Services;
using Carrefas.FinancialFlow.Domain.Queue;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Carrefas.FinancialFlow.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            InversionOfControl.RegisterServices(services);
            CoreIoC.CoreIoCServices(services);            

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            return services;
        }
    }
}
