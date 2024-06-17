using Carrefas.Core.Interfaces;
using Carrefas.Core.Notifications;
using Microsoft.Extensions.DependencyInjection;

namespace Carrefas.Core.IoC
{
    public static class CoreIoC
    {
        public static void CoreIoCServices(IServiceCollection services)
        {
            services.AddScoped<INotifier, Notifier>();
        }
    }
}
