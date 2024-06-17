using Carrefas.Core.DomainObjects;
using Carrefas.Core.Interfaces;

namespace Carrefas.FinancialFlow.Domain.Entities
{
    public class FinancialPosting : Entity, IAggregateRoot
    {
        public FinancialPosting(decimal value, 
                                DateTime date, 
                                bool isCredit)
        {
            Value = value;
            Date = date;
            IsCredit = isCredit;
        }

        public decimal Value { get; private set; }
        public DateTime Date { get; private set; }
        public bool IsCredit { get; private set; }
    }
}
