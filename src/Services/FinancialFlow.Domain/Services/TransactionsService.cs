using Carrefas.Core.Services;
using FinancialFlow.Core.Interfaces;
using FinancialFlow.Core.Queue;
using FinancialFlow.Domain.Entities;
using FinancialFlow.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace FinancialFlow.Domain.Services
{
    public class FinancialTransactionService : BaseService, IFinancialTransactionService
    {
        private readonly IFinancialTransactionRepository _transactionsRepository;
        private readonly ILogger<FinancialTransactionService> _logger;
        public FinancialTransactionService(INotifier notifier,
                                   IFinancialTransactionRepository transactionsRepository,
                                   ILogger<FinancialTransactionService> logger) : base(notifier)
        {
            _transactionsRepository = transactionsRepository;
            _logger = logger;
        }

        public async Task<bool> AddFinancialTransaction(FinancialTransaction financialTransaction)
        {
            financialTransaction.SetId();
            await _transactionsRepository.AddFinancialTransaction(financialTransaction);
            var commit = await _transactionsRepository.UnitOfWork.Commit();

            if (commit)
            {
                _logger.LogInformation("Lançamento realizado com sucesso");
                //TODO: CHAMAR O METODO CONSOLIDAÇÃO
            }

            return commit;
        }


        public async Task<bool> AddFinancialTransactionQueue(FinancialTransaction financialTransaction)
        {         

            await QueueFlow.EnviacardParaFila(financialTransaction, "ProjetoQueue");
            return true;
        }

        public void Dispose() => _transactionsRepository.Dispose();
    }
}
