using AutoMapper;
using Carrefas.FinancialFlow.Application.Interfaces;
using Carrefas.FinancialFlow.Application.Models;
using Carrefas.FinancialFlow.Domain.Entities;
using Carrefas.FinancialFlow.Domain.Interfaces;

namespace Carrefas.FinancialFlow.Application.Services
{
    public class FinancialFlowAppService : IFinancialFlowAppService
    {
        private readonly IFinancialFlowService _financialFlowService;
        private readonly IMapper _mapper;

        public FinancialFlowAppService(IFinancialFlowService financialFlowService, IMapper mapper)
        {
            _financialFlowService = financialFlowService;
            _mapper = mapper;
        }

        public async Task<bool> AddFinancialPosting(FinancialPostingModel financialPostingModel)
        {
            return await _financialFlowService.AddFinancialPosting(_mapper.Map<FinancialPosting>(financialPostingModel));
        }
    }
}
