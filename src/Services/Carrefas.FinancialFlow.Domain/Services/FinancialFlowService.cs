﻿using Carrefas.Core.Interfaces;
using Carrefas.Core.Services;
using Carrefas.FinancialFlow.Domain.Entities;
using Carrefas.FinancialFlow.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Carrefas.FinancialFlow.Domain.Services
{
    public class FinancialFlowService : BaseService, IFinancialFlowService
    {
        private readonly ILogger<FinancialFlowService> _logger;
        private readonly IFinancialFlowRepository _financialFlowRepository;

        public FinancialFlowService(INotifier notifier, 
                                    ILogger<FinancialFlowService> logger, 
                                    IFinancialFlowRepository financialFlowRepository) : base(notifier)
        {
            _logger = logger;
            _financialFlowRepository = financialFlowRepository;
        }

        public async Task<bool> AddFinancialPosting(FinancialPosting financialPosting)
        {
            await _financialFlowRepository.AddFinancialPosting(financialPosting);

            var commit = await _financialFlowRepository.UnitOfWork.Commit();

            if (commit)
            {
                _logger.LogInformation("Lançamento realizado com sucesso");
                await UpdateConsolidatedDaily(financialPosting.Date);
            }

            return commit;
        }

        private async Task<bool> UpdateConsolidatedDaily(DateTime date)
        {
            var financialPosting = await _financialFlowRepository.SearchFinancialPosting(c => c.Date == date);  

            var balance = financialPosting.Sum(l => l.IsCredit ? l.Value : -l.Value);           

            var consolidated = new DailyConsolidated(date, balance);

            _financialFlowRepository.UpdateConsolidatedDaily(consolidated);

            var commit = await _financialFlowRepository.UnitOfWork.Commit();

            if (commit)
            {
                _logger.LogInformation("Consolidado atualizado com sucesso");
                return commit;
            };

            _logger.LogError("Error ao atualizar Consolidado... :)");
            return false;
        }

        public async Task<DailyConsolidated> GetDailyConsolidated(DateTime date) => await _financialFlowRepository.GetDailyConsolidated(date);

    }
}