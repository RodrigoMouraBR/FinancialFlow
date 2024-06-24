using System.ComponentModel.DataAnnotations;

namespace FinancialFlow.Application.Models
{
    public class FinancialTransactionModel
    {
        [Key]
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; } = string.Empty;
        public int TransactionType { get; set; } = int.MinValue;
    }
}
