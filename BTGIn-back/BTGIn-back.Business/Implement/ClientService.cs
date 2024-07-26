using BTGIn_back.Business.Contracts;
using BTGIn_back.Entitites;
using BTGIn_back.Repositories.Contracts;

namespace BTGIn_back.Business.Implement
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task CreateAsync(Client entity)
        {
            await _clientRepository.CreateAsync(entity);
        }

        public async Task DeleteAsync(string id)
        {
            await _clientRepository.DeleteAsync(id);
        }

        public async Task<List<Client>> GetAllAsync()
        {
            return await _clientRepository.GetAllAsync();
        }

        public async Task<Client> GetAsync(string id)
        {
            return await _clientRepository.GetAsync(id);
        }

        public async Task UpdateAsync(string id, Client entity)
        {
            await _clientRepository.UpdateAsync(id, entity);
        }
    }
}
