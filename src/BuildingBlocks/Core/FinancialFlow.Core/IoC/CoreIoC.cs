using FinancialFlow.Core.Interfaces;
using FinancialFlow.Core.Notifications;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime;

namespace FinancialFlow.Core.IoC
{
    public static class CoreIoC
    {
        public static void CoreIoCServices(IServiceCollection services)
        {
            services.AddScoped<INotifier, Notifier>();

            
        }
    }
}
