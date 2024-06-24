using FinancialFlow.Core.EnvironmentVariable;
using FinancialFlow.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FinancialFlow.Data.Contexts
{
    public static class FinancialFlowConnections
    {
        public static IServiceCollection AddConnectionUseNpgsql(this IServiceCollection services, IConfiguration configuration)
        {


            var rabbitMqHost = EnvironmentVariableRepository.rabbithost;         
            var postgresHost = EnvironmentVariableRepository.postgrehost;        
            var postgresUser = EnvironmentVariableRepository.postgreuser;        
            var postgresPassword = EnvironmentVariableRepository.postgrepassword;
            var postgresDb = EnvironmentVariableRepository.postgredb;            

            var connectionString = $"Host={postgresHost};Database={postgresDb};Username={postgresUser};Password={postgresPassword}";

            services.AddDbContext<FinancialFlowContext>(options =>
                options.UseNpgsql(connectionString));

            return services;


            //services.AddEntityFrameworkNpgsql().AddDbContext<FinancialFlowContext>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
            //return services;
        }
    }
}
