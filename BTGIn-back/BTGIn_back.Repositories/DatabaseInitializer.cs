using BTGIn_back.Entitites;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BTGIn_back.Repositories
{
    public class DatabaseInitializer
    {
        private readonly IMongoDatabase _database;

        public DatabaseInitializer(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task InitializeAsync()
        {
            string collectionName = "fund";
            var collectionNames = await _database.ListCollectionNames().ToListAsync();
            if (!collectionNames.Contains(collectionName))
            {
                await _database.CreateCollectionAsync(collectionName);
                var collection = _database.GetCollection<Fund>(collectionName);
                var initialData = new List<Fund>
                {
                    new() { Name = "FPV_BTG_PACTUAL_RECAUDADORA", MinimumRegistrationAmount = 75000, Category = "FPV" },
                    new() { Name = "FPV_BTG_PACTUAL_ECOPETROL", MinimumRegistrationAmount = 125000, Category = "FPV" },
                    new() { Name = "DEUDAPRIVADA", MinimumRegistrationAmount = 50000, Category = "FIC" },
                    new() { Name = "FDO-ACCIONES", MinimumRegistrationAmount = 250000, Category = "FIC" },
                    new() { Name = "FPV_BTG_PACTUAL_DINAMICA", MinimumRegistrationAmount = 100000, Category = "FPV" }
                };

                await collection.InsertManyAsync(initialData);
            }
                        
            collectionName = "client";
            collectionNames = await _database.ListCollectionNames().ToListAsync();
            if (!collectionNames.Contains(collectionName))
            {
                await _database.CreateCollectionAsync(collectionName);
                var collection = _database.GetCollection<Client>(collectionName);
                var initialData = new List<Client>
                {
                    new() { Name = "uno", Identification = 123, Cash = 500000, Funds = [] }
                };

                await collection.InsertManyAsync(initialData);
            }
        }
    }
}