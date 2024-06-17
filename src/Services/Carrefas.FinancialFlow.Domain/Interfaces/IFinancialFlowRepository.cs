using Carrefas.Core.Interfaces;
using Carrefas.FinancialFlow.Domain.Entities;
using System.Linq.Expressions;

namespace Carrefas.FinancialFlow.Domain.Interfaces
{
    public interface IFinancialFlowRepository : IRepository<FinancialPosting>
    {
        Task AddFinancialPosting(FinancialPosting financialPosting);   
        Task<IEnumerable<FinancialPosting>> SearchFinancialPosting(Expression<Func<FinancialPosting, bool>> predicate);
        Task<IEnumerable<DailyConsolidated>> SearchDailyConsolidated(Expression<Func<DailyConsolidated, bool>> predicate);
        void UpdateConsolidatedDaily(DailyConsolidated dailyConsolidated);
        Task<DailyConsolidated> GetDailyConsolidated(DateTime date);
    }
}
