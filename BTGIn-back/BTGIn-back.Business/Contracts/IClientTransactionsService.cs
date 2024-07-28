using BTGIn_back.Entitites.DTO.Request;
using BTGIn_back.Entitites.DTO.Response;

namespace BTGIn_back.Business.Contracts
{
    public interface IClientTransactionsService
    {
        Task FundInscription(FundInscriptionRequest fundInscriptionRequest);
        Task FundDisenrollment(FundDisenrollmentRequest fundDisenrollmentRequest);
        Task<TransactionsHistoryResponse> GetTransactionsHistory(int clientIdentification);
    }
}
