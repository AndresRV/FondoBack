using BTGIn_back.Entitites;
using BTGIn_back.Repositories.Contracts;
using MongoDB.Driver;

namespace BTGIn_back.Repositories.Implement
{
    public class ClientRepository : IClientRepository
    {
        private readonly IMongoCollection<Client> _entities;

        public ClientRepository(IMongoDatabase database)
        {
            _entities = database.GetCollection<Client>("client");
        }

        public async Task CreateAsync(Client entity)
        {
            await _entities.InsertOneAsync(entity);
        }

        public async Task DeleteAsync(string id)
        {
            await _entities.DeleteOneAsync(entity => entity.Id == id);
        }

        public async Task<List<Client>> GetAllAsync()
        {
            return await _entities.Find(entity => true).ToListAsync();
        }

        public async Task<Client> GetAsync(string id)
        {
            return await _entities.Find(entity => entity.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(string id, Client entity)
        {
            await _entities.ReplaceOneAsync(entity => entity.Id == id, entity);
        }
    }
}
