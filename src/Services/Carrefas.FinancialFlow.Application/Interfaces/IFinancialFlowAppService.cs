using Carrefas.FinancialFlow.Application.Models;

namespace Carrefas.FinancialFlow.Application.Interfaces
{
    public interface IFinancialFlowAppService
    {
        Task<bool> AddFinancialPosting(FinancialPostingModel financialPostingModel);
    }
}
