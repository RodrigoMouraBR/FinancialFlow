using FinancialFlow.API.Controllers;
using FinancialFlow.Application.Interfaces;
using FinancialFlow.Application.Models;
using FinancialFlow.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinancialFlow.API.V1.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/financial-transaction/")]
    public class FinancialTransactionController : MainController
    {
        private readonly IFinancialTransactionAppService _appService;
        public FinancialTransactionController(INotifier notifier, IFinancialTransactionAppService appService) : base(notifier)
        {
            _appService = appService;
        }

        [HttpPost("add-financial")]
        public async Task<IActionResult> AddFinancialTransaction([FromBody] FinancialTransactionModel financialTransactionModel)
        {
            var result = await _appService.AddFinancialTransactionQueue(financialTransactionModel);
            return CustomResponse(result);
        }

        [HttpGet("MonthlyConsolidated")]
        public async Task<IActionResult> GetMonthlyConsolidatedReportAsync()
        {
            var result = await _appService.GetMonthlyConsolidatedReportAsync();
            return CustomResponse(result);
        }

        [HttpGet("DailyConsolidatedReport")]
        public async Task<IActionResult> GetDailyConsolidatedReportAsync()
        {
            var result = await _appService.GetDailyConsolidatedReportAsync();
            return CustomResponse(result);
        }

        [HttpGet("YearlyConsolidatedReport")]
        public async Task<IActionResult> GetYearlyConsolidatedReportAsync()
        {
            var result = await _appService.GetYearlyConsolidatedReportAsync();
            return CustomResponse(result);
        }
    }
}
