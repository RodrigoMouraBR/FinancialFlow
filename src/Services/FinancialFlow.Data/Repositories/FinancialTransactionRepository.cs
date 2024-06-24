using FinancialFlow.Core.Interfaces;
using FinancialFlow.Data.Contexts;
using FinancialFlow.Domain.DTO;
using FinancialFlow.Domain.Entities;
using FinancialFlow.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FinancialFlow.Data.Repositories
{
    public class FinancialTransactionRepository : IFinancialTransactionRepository
    {
        private readonly FinancialFlowContext _context;
        public FinancialTransactionRepository(FinancialFlowContext context) => _context = context;

        public IUnitOfWork UnitOfWork => _context;

        public async Task AddFinancialTransaction(FinancialTransaction financialTransaction) =>
            await _context.FinancialTransactions.AddAsync(financialTransaction);
        public async Task<IEnumerable<FinancialTransaction>> SearchFinancialTransaction(Expression<Func<FinancialTransaction, bool>> predicate) =>
            await _context.FinancialTransactions.AsNoTracking().Where(predicate).ToListAsync().ConfigureAwait(false);
        public async Task<IEnumerable<MonthlyConsolidatedReport>> GetMonthlyConsolidatedReportAsync()
        {
            var results = await _context.FinancialTransactions
                .GroupBy(t => new { t.CustomerId, YearMonth = new DateTime(t.Date.Year, t.Date.Month, 1) })
                .Select(g => new MonthlyConsolidatedReport
                {
                    CustomerId = g.Key.CustomerId,
                    YearMonth = g.Key.YearMonth,
                    TotalAmount = g.Sum(t => t.Amount)
                })
                .OrderBy(r => r.CustomerId)
                .ThenBy(r => r.YearMonth)
                .ToListAsync();
            return results;
        }
        public async Task<IEnumerable<DailyConsolidatedReport>> GetDailyConsolidatedReportAsync()
        {
            var dailyResults = await _context.FinancialTransactions
            .GroupBy(t => new { t.CustomerId, t.Date.Date })
            .Select(g => new DailyConsolidatedReport
            {
                CustomerId = g.Key.CustomerId,
                Date = g.Key.Date,
                TotalAmount = g.Sum(t => t.Amount)
            })
            .OrderBy(r => r.CustomerId)
            .ThenBy(r => r.Date)
            .ToListAsync();
            return dailyResults;
        }

        public async Task<IEnumerable<YearlyConsolidatedReport>> GetYearlyConsolidatedReportAsync()
        {
            var yearlyResults = await _context.FinancialTransactions
            .GroupBy(t => new { t.CustomerId, t.Date.Year })
            .Select(g => new YearlyConsolidatedReport
            {
                CustomerId = g.Key.CustomerId,
                Year = g.Key.Year,
                TotalAmount = g.Sum(t => t.Amount)
            })
            .OrderBy(r => r.CustomerId)
            .ThenBy(r => r.Year)
            .ToListAsync();

            return yearlyResults;
        }

        public void Dispose() => _context.Dispose();
    }
}
