using Carrefas.Core.Interfaces;
using Carrefas.FinancialFlow.Application.Interfaces;
using Carrefas.FinancialFlow.Application.Models;
using Microsoft.AspNetCore.Mvc;
using Wms.OM.API.Controllers;

namespace Carrefas.FinancialFlow.API.V1.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/financialFlow/")]
    public class FinancialFlowController : MainController
    {
        private readonly IFinancialFlowAppService _financialFlowAppService;
        public FinancialFlowController(INotifier notifier, IFinancialFlowAppService financialFlowAppService) : base(notifier)
        {
            _financialFlowAppService = financialFlowAppService;
        }

        [HttpPost("add-financial-posting")]
        public async Task<IActionResult> AddFinancialPosting([FromBody]FinancialPostingModel financialPostingModel)
        {
            var result = await _financialFlowAppService.AddFinancialPosting(financialPostingModel);
            return CustomResponse(result);
        }
    }
}
