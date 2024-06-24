using FinancialFlow.Core.DomainObjects;
using FinancialFlow.Core.DomainObjects.Enums;
using FinancialFlow.Core.Interfaces;

namespace FinancialFlow.Domain.Entities
{
    public class FinancialTransaction : Entity, IAggregateRoot
    {
        public FinancialTransaction(Guid customerId, 
                           DateTime date, 
                           decimal amount, 
                           string description, 
                           ETransactionType transactionType)
        {
            CustomerId = customerId;
            Date = date;
            Amount = amount;
            Description = description;
            TransactionType = transactionType;
        }
        protected FinancialTransaction() { }      
        public Guid CustomerId { get; private set; }
        public DateTime Date { get; private set; }
        public decimal Amount { get; private set; }
        public string Description { get; private set; }
        public ETransactionType TransactionType { get; private set; }   
        

        public void SetId() => Id = Guid.NewGuid();
       

    }
}
