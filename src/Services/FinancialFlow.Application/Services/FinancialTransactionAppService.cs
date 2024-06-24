using AutoMapper;
using FinancialFlow.Application.Interfaces;
using FinancialFlow.Application.Models;
using FinancialFlow.Domain.Entities;
using FinancialFlow.Domain.Interfaces;

namespace FinancialFlow.Application.Services
{
    public class FinancialTransactionAppService : IFinancialTransactionAppService
    {
        private readonly IFinancialTransactionService _financialTransactionService;
        private readonly IFinancialTransactionRepository _financialTransactionRepository;
        private readonly IMapper _mapper;

        public FinancialTransactionAppService(IFinancialTransactionService financialTransactionService, 
                                              IMapper mapper, 
                                              IFinancialTransactionRepository financialTransactionRepository)
        {
            _financialTransactionService = financialTransactionService;
            _mapper = mapper;
            _financialTransactionRepository = financialTransactionRepository;
        }

        public async Task<bool> AddFinancialTransactionQueue(FinancialTransactionModel financialTransaction) =>
          await _financialTransactionService.AddFinancialTransactionQueue(_mapper.Map<FinancialTransaction>(financialTransaction));

        public async Task<bool> AddFinancialTransaction(FinancialTransactionModel financialTransaction) =>
          await _financialTransactionService.AddFinancialTransaction(_mapper.Map<FinancialTransaction>(financialTransaction));       

        public async Task<IEnumerable<MonthlyConsolidatedReportModel>> GetMonthlyConsolidatedReportAsync() => 
            _mapper.Map<IEnumerable<MonthlyConsolidatedReportModel>>(await _financialTransactionRepository.GetMonthlyConsolidatedReportAsync());        

        public async Task<IEnumerable<DailyConsolidatedReportModel>> GetDailyConsolidatedReportAsync() =>
            _mapper.Map<IEnumerable<DailyConsolidatedReportModel>>(await _financialTransactionRepository.GetDailyConsolidatedReportAsync());

        public async Task<IEnumerable<YearlyConsolidatedReportModel>> GetYearlyConsolidatedReportAsync() =>
            _mapper.Map<IEnumerable<YearlyConsolidatedReportModel>>(await _financialTransactionRepository.GetYearlyConsolidatedReportAsync());

        public void Dispose() => _financialTransactionService.Dispose();
    }
}
