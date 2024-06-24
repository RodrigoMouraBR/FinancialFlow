using FinancialFlow.Application.Interfaces;
using FinancialFlow.Application.Services;
using FinancialFlow.Data.Contexts;
using FinancialFlow.Data.Repositories;
using FinancialFlow.Domain.Interfaces;
using FinancialFlow.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FinancialFlow.Application.IoC
{
    public static class InversionOfControl
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IFinancialTransactionAppService, FinancialTransactionAppService>();           
            services.AddScoped<IFinancialTransactionService, FinancialTransactionService>();
            services.AddScoped<IFinancialTransactionRepository, FinancialTransactionRepository>();
            services.AddScoped<FinancialFlowContext>();
        }
    }
}
