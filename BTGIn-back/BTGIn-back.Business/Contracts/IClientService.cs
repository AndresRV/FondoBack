using BTGIn_back.Entitites;

namespace BTGIn_back.Business.Contracts
{
    public interface IClientService
    {
        Task<List<Client>> GetAllAsync();
        Task<Client> GetAsync(string id);
        Task CreateAsync(Client entity);
        Task UpdateAsync(string id, Client entity);
        Task DeleteAsync(string id);
    }
}
