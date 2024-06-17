using Carrefas.Core.DomainObjects;

namespace Carrefas.FinancialFlow.Domain.Entities
{
    public class DailyConsolidated : Entity
    {
        public DailyConsolidated(DateTime date, decimal balance)
        {
            Date = date;
            Balance = balance;
        }
        public DateTime Date { get; private set; }
        public decimal Balance { get; private set; }
    }
}
