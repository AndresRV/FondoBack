using BTGIn_back.Entitites;
using BTGIn_back.Repositories.Contracts;
using MongoDB.Driver;

namespace BTGIn_back.Repositories.Implement
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly IMongoCollection<Transaction> _entity;

        public TransactionRepository(IMongoDatabase database)
        {
            _entity = database.GetCollection<Transaction>("transaction");
        }

        public async Task CreateAsync(Transaction transaction)
        {
            await _entity.InsertOneAsync(transaction);
        }
    }
}
