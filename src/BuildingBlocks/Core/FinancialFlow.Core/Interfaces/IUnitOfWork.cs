namespace FinancialFlow.Core.Interfaces
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
