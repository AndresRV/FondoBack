using BTGIn_back.Entitites;
using BTGIn_back.Repositories.Contracts;
using MongoDB.Driver;

namespace BTGIn_back.Repositories.Implement
{
    public class FundRepository : IFundRepository
    {
        private readonly IMongoCollection<Fund> _entity;

        public FundRepository(IMongoDatabase database)
        {
            _entity = database.GetCollection<Fund>("fund");
        }

        public async Task<Fund> GetByNameAsync(string name)
        {
            return await _entity.Find(fun => fun.Name == name).FirstOrDefaultAsync();
        }

        public async Task<List<Fund>> GetAll()
        {
            return await _entity.Find(fund => true).ToListAsync();
        }
    }
}
