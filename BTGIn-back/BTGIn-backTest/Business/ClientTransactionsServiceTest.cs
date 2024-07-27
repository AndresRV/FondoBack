using BTGIn_back.Business.Implement;
using BTGIn_back.Entitites;
using BTGIn_back.Repositories.Contracts;
using Moq;
using BTGIn_back.Business.Exceptions;
using BTGIn_backTest.Utils;
using BTGIn_back.Entitites.DTO.Request;

namespace BTGIn_backTest.Business
{
    [TestClass]
    public class ClientTransactionsServiceTest
    {
        private readonly Mock<IClientRepository> _mockClientRepository = new();
        private readonly Mock<IFundRepository> _mockFundRepository = new();
        private readonly Mock<ITransactionRepository> _mockTransactionRepository = new();
        private readonly ClientTransactionsService _clientTransactionsService;

        public ClientTransactionsServiceTest()
        {
            _clientTransactionsService = new(_mockClientRepository.Object, _mockFundRepository.Object, _mockTransactionRepository.Object);
        }            

        [TestMethod]
        public async Task FundInscriptionOldUserOk()
        {
            _mockClientRepository
                .Setup(x => x.GetByIdentificationAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(MockResponsesGenerator.GetByIdentificationAsyncResult()));
            _mockFundRepository
                .Setup(x => x.GetByNameAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(MockResponsesGenerator.GetByNameAsyncResult()));
            
            await _clientTransactionsService.FundInscription(MockRequestGenerator.GetFundInscriptionRequest(alterFundName: true));

            _mockClientRepository.Verify(x => x.UpdateAsync(It.IsAny<string>(), It.IsAny<Client>()), Times.Once);
            _mockTransactionRepository.Verify(x => x.CreateAsync(It.IsAny<Transaction>()), Times.Once);
        }

        [TestMethod]
        public async Task FundInscriptionNewUserOk()
        {
            _mockFundRepository
                .Setup(x => x.GetByNameAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(MockResponsesGenerator.GetByNameAsyncResult()));

            await _clientTransactionsService.FundInscription(MockRequestGenerator.GetFundInscriptionRequest());

            _mockClientRepository.Verify(x => x.CreateAsync(It.IsAny<Client>()), Times.Once);
            _mockTransactionRepository.Verify(x => x.CreateAsync(It.IsAny<Transaction>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(FundAlreadyRegistredException))]
        public async Task FundInscriptionFundAlreadyRegistredException()
        {
            _mockClientRepository
                .Setup(x => x.GetByIdentificationAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(MockResponsesGenerator.GetByIdentificationAsyncResult()));
            _mockFundRepository
                .Setup(x => x.GetByNameAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(MockResponsesGenerator.GetByNameAsyncResult()));

            await _clientTransactionsService.FundInscription(MockRequestGenerator.GetFundInscriptionRequest());
        }
        
        [TestMethod]
        [ExpectedException(typeof(InsufficientCashException))]
        public async Task FundInscriptionInsufficientCashException()
        {
            _mockClientRepository
                .Setup(x => x.GetByIdentificationAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(MockResponsesGenerator.GetByIdentificationAsyncResult()));
            _mockFundRepository
                .Setup(x => x.GetByNameAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(MockResponsesGenerator.GetByNameAsyncResult()));

            await _clientTransactionsService.FundInscription(MockRequestGenerator.GetFundInscriptionRequest(true, true));
        }
    }
}
