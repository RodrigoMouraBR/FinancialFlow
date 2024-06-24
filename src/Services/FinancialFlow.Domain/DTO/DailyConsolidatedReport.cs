namespace FinancialFlow.Domain.DTO
{
    public class DailyConsolidatedReport
    {
        public Guid CustomerId { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
