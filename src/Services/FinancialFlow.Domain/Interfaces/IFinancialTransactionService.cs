using FinancialFlow.Domain.Entities;

namespace FinancialFlow.Domain.Interfaces
{    
    public interface IFinancialTransactionService : IDisposable
    {
        Task<bool> AddFinancialTransaction(FinancialTransaction financialTransaction);
        Task<bool> AddFinancialTransactionQueue(FinancialTransaction financialTransaction);
    }
}
