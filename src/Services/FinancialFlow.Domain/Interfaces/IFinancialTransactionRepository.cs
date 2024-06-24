using FinancialFlow.Core.Interfaces;
using FinancialFlow.Domain.DTO;
using FinancialFlow.Domain.Entities;
using System.Linq.Expressions;

namespace FinancialFlow.Domain.Interfaces
{
    public interface IFinancialTransactionRepository : IRepository<FinancialTransaction>
    {
        Task AddFinancialTransaction(FinancialTransaction financialTransaction);
        Task<IEnumerable<FinancialTransaction>> SearchFinancialTransaction(Expression<Func<FinancialTransaction, bool>> predicate);
        Task<IEnumerable<MonthlyConsolidatedReport>> GetMonthlyConsolidatedReportAsync();
        Task<IEnumerable<DailyConsolidatedReport>> GetDailyConsolidatedReportAsync();
        Task<IEnumerable<YearlyConsolidatedReport>> GetYearlyConsolidatedReportAsync();
    }
}
