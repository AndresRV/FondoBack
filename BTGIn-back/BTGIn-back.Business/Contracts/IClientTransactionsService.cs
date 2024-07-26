using BTGIn_back.Entitites;
using BTGIn_back.Entitites.DTO.Request;

namespace BTGIn_back.Business.Contracts
{
    public interface IClientTransactionsService
    {
        Task FundInscription(FundInscriptionRequest fundInscriptionRequest);
    }
}
