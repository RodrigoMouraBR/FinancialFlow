using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialFlow.Application.Models
{
    public class YearlyConsolidatedReportModel
    {
        public Guid CustomerId { get; set; }
        public int Year { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
