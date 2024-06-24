namespace FinancialFlow.Application.Models
{
    public class MonthlyConsolidatedReportModel
    {
        public Guid CustomerId { get; set; }
        public DateTime YearMonth { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
