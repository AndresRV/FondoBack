using BTGIn_back.Entitites;
using BTGIn_back.Repositories.Contracts;
using MongoDB.Driver;

namespace BTGIn_back.Repositories.Implement
{
    public class ClientRepository : IClientRepository
    {
        private readonly IMongoCollection<Client> _entity;

        public ClientRepository(IMongoDatabase database)
        {
            _entity = database.GetCollection<Client>("client");
        }

        public async Task<Client> GetByIdentificationAsync(int identification)
        {
            return await _entity.Find(client => client.Identification == identification).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Client client)
        {
            await _entity.InsertOneAsync(client);
        }

        public async Task UpdateAsync(string id, Client client)
        {
            await _entity.ReplaceOneAsync(client => client.Id == id, client);
        }
    }
}
