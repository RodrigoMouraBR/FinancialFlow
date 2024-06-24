namespace FinancialFlow.Application.Models
{
    public class DailyConsolidatedReportModel
    {
        public Guid CustomerId { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
