using Carrefas.Core.Interfaces;
using Carrefas.FinancialFlow.Domain.Entities;
using Carrefas.FinancialFlow.Domain.Interfaces;
using Carrefas.FinancialFlow.Domain.Services;
using Carrefas.FinancialFlow.Tests.HumanData;
using Microsoft.Extensions.Logging;
using Moq;

namespace Carrefas.FinancialFlow.Tests.FinancialFlow
{
    [Collection(nameof(CarrefasBogusCollection))]
    public class UnitTest1
    {
        readonly CarrefasBogusFixture _carrefasBogusFixture;

        public UnitTest1(CarrefasBogusFixture carrefasBogusFixture) => _carrefasBogusFixture = carrefasBogusFixture;        

        [Fact(DisplayName = "Adicionar com sucesso")]
        [Trait("FinancialFlow", "Service Mock Tests")]
        public async Task ShouldFinancialFlowSuccessfully()
        {
            //ARRAGE
            var financialPosting = _carrefasBogusFixture.GenerateFinancialPostingValid();
            var repositoryMock = new Mock<IFinancialFlowRepository>();
            var notifierMock = new Mock<INotifier>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var loggerMock = new Mock<ILogger<FinancialFlowService>>();

            repositoryMock.Setup(repo => repo.UnitOfWork).Returns(mockUnitOfWork.Object);
            repositoryMock.Setup(repo => repo.AddFinancialPosting(It.IsAny<FinancialPosting>())).Returns(Task.CompletedTask);
            var financialPostingService = new FinancialFlowService(notifierMock.Object, loggerMock.Object, repositoryMock.Object);

            //ACT
            await financialPostingService.AddFinancialPosting(financialPosting);

            //ASSERT
            repositoryMock.Verify(repo => repo.AddFinancialPosting(financialPosting), Times.Once);
            mockUnitOfWork.Verify(uow => uow.Commit(), Times.Once);
        }
    }
}