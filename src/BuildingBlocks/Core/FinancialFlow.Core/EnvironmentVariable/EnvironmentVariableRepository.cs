namespace FinancialFlow.Core.EnvironmentVariable
{
    public class EnvironmentVariableRepository 
    {      
        public static string rabbithost => Environment.GetEnvironmentVariable("RABBITMQ_HOST") ?? "localhost";
        public static string postgrehost => Environment.GetEnvironmentVariable("POSTGRES_HOST") ?? "localhost";
        public static string postgreuser => Environment.GetEnvironmentVariable("POSTGRES_USER") ?? "guest";
        public static string postgrepassword => Environment.GetEnvironmentVariable("POSTGRES_PASSWORD") ?? "guest";
        public static string postgredb => Environment.GetEnvironmentVariable("POSTGRES_DB") ?? "financialflow";
    }
}
