using BTGIn_back.Entitites;

namespace BTGIn_back.Repositories.Contracts
{
    public interface IClientRepository
    {
        Task<List<Client>> GetAllAsync();
        Task<Client> GetAsync(string id);
        Task CreateAsync(Client entity);
        Task UpdateAsync(string id, Client entity);
        Task DeleteAsync(string id);
    }
}
