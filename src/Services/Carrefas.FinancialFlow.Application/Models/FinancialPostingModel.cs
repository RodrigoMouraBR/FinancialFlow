namespace Carrefas.FinancialFlow.Application.Models
{
    public class FinancialPostingModel
    {
        public decimal Value { get; set; }
        public DateTime Date { get; set; }
        public bool IsCredit { get; set; }
    }
}
