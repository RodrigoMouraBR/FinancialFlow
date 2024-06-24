namespace FinancialFlow.Domain.DTO
{
    public class YearlyConsolidatedReport
    {
        public Guid CustomerId { get; set; }
        public int Year { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
