namespace FinancialFlow.Domain.DTO
{
    public class MonthlyConsolidatedReport
    {
        public Guid CustomerId { get; set; }
        public DateTime YearMonth { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
