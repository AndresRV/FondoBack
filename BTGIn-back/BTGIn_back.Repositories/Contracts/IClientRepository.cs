using BTGIn_back.Entitites;

namespace BTGIn_back.Repositories.Contracts
{
    public interface IClientRepository
    {
        Task<Client> GetByIdentificationAsync(int identification);
        Task CreateAsync(Client client);
        Task UpdateAsync(string id, Client client);
    }
}
