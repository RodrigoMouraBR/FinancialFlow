using Carrefas.FinancialFlow.Application.Interfaces;
using Carrefas.FinancialFlow.Application.Services;
using Carrefas.FinancialFlow.Data.Contexts;
using Carrefas.FinancialFlow.Data.Repositories;
using Carrefas.FinancialFlow.Domain.Interfaces;
using Carrefas.FinancialFlow.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Carrefas.FinancialFlow.Application.IoC
{
    public static class InversionOfControl
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IFinancialFlowService, FinancialFlowService>();
            services.AddScoped<IFinancialFlowAppService, FinancialFlowAppService>();
            services.AddScoped<IFinancialFlowRepository, FinancialFlowRepository>();
            services.AddScoped<CarrefasContext>();
        }
    }
}
