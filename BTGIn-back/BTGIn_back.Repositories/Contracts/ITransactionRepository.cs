using BTGIn_back.Entitites;

namespace BTGIn_back.Repositories.Contracts
{
    public interface ITransactionRepository
    {
        Task CreateAsync(Transaction transaction);
        Task<List<Transaction>> GetTransactionsByClientIdentification(int clientIdentification);
    }
}
