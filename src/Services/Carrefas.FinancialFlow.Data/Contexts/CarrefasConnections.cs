using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Carrefas.FinancialFlow.Data.Contexts
{
    public static class CarrefasConnections
    {
        public static IServiceCollection AddConnectionUseNpgsql(this IServiceCollection services, IConfiguration configuration)
        {       
            services.AddEntityFrameworkNpgsql().AddDbContext<CarrefasContext>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
            return services;
        }
    }
}
