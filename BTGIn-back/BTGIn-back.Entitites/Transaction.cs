using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace BTGIn_back.Entitites
{
    public class Transaction
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public required string Type { get; set; }
        public required double Amount { get; set; } //opcional
        public DateTime Date { get; set; }
        public bool IsAcepted { get; set; }
        public required Client Client { get; set; }
        public required Fund Fund { get; set; }
    }
}
