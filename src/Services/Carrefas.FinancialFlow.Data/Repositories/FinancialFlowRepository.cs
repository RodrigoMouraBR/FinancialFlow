using Carrefas.Core.Interfaces;
using Carrefas.FinancialFlow.Data.Contexts;
using Carrefas.FinancialFlow.Domain.Entities;
using Carrefas.FinancialFlow.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Carrefas.FinancialFlow.Data.Repositories
{
    public class FinancialFlowRepository : IFinancialFlowRepository
    {

        private readonly CarrefasContext _context;

        public FinancialFlowRepository(CarrefasContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task AddFinancialPosting(FinancialPosting financialPosting) => await _context.FinancialPosting.AddAsync(financialPosting);

        public async Task<DailyConsolidated> GetDailyConsolidated(DateTime date) => await _context.DailyConsolidated.AsNoTracking().FirstOrDefaultAsync(p => p.Date == date);

        public void UpdateConsolidatedDaily(DailyConsolidated dailyConsolidated) => _context.DailyConsolidated.Update(dailyConsolidated);

        public async Task<IEnumerable<FinancialPosting>> SearchFinancialPosting(Expression<Func<FinancialPosting, bool>> predicate)
        {
           return await _context.FinancialPosting.AsNoTracking().Where(predicate).ToListAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<DailyConsolidated>> SearchDailyConsolidated(Expression<Func<DailyConsolidated, bool>> predicate)
        {
            return await _context.DailyConsolidated.AsNoTracking().Where(predicate).ToListAsync().ConfigureAwait(false);
        }

        public void Dispose()
        {
            _context.Dispose();
        }      
    }
}
