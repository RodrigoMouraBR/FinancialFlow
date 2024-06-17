using AutoMapper;
using Carrefas.FinancialFlow.Application.Models;
using Carrefas.FinancialFlow.Domain.Entities;

namespace Carrefas.FinancialFlow.Application.AutoMapper
{
    public class FinancialFlowMappingConfig : Profile
    {
        public FinancialFlowMappingConfig()
        {
            CreateMap<DailyConsolidatedModel, DailyConsolidated>().ReverseMap();
            CreateMap<FinancialPostingModel, FinancialPosting>().ReverseMap();
        }
    }
}
