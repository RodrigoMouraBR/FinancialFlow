namespace FinancialFlow.Core.Interfaces.EnvironmentVariable
{
    internal interface IEnvironmentVariableRepository
    {
        string GetEnvironmentVariable(string variableName);
    }
}
