using AutoMapper;
using FinancialFlow.Application.Models;
using FinancialFlow.Domain.DTO;
using FinancialFlow.Domain.Entities;

namespace FinancialFlow.Application.AutoMapper
{
    public class FinancialFlowMappingConfig : Profile
    {
        public FinancialFlowMappingConfig()
        {
            CreateMap<FinancialTransactionModel, FinancialTransaction>().ReverseMap();
            CreateMap<MonthlyConsolidatedReportModel, MonthlyConsolidatedReport>().ReverseMap();
            CreateMap<DailyConsolidatedReportModel, DailyConsolidatedReport>().ReverseMap();
            CreateMap<YearlyConsolidatedReportModel, YearlyConsolidatedReport>().ReverseMap();
        }
    }
}
