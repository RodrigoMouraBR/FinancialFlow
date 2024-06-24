using FinancialFlow.Application.Models;

namespace FinancialFlow.Application.Interfaces
{
    public interface IFinancialTransactionAppService : IDisposable
    {
        Task<bool> AddFinancialTransaction(FinancialTransactionModel financialTransaction);
        Task<bool> AddFinancialTransactionQueue(FinancialTransactionModel financialTransaction);
        Task<IEnumerable<MonthlyConsolidatedReportModel>> GetMonthlyConsolidatedReportAsync();
        Task<IEnumerable<DailyConsolidatedReportModel>> GetDailyConsolidatedReportAsync();
        Task<IEnumerable<YearlyConsolidatedReportModel>> GetYearlyConsolidatedReportAsync();
    }
}
