using Carrefas.FinancialFlow.Domain.Entities;

namespace Carrefas.FinancialFlow.Domain.Interfaces
{
    public interface IFinancialFlowService
    {
        Task<bool> AddFinancialPosting(FinancialPosting financialPosting);

        Task<DailyConsolidated> GetDailyConsolidated(DateTime date);

        Task<bool> Atualizar(DailyConsolidated dailyConsolidated);
    }
}
