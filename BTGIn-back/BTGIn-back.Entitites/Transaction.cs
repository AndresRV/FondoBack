using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace BTGIn_back.Entitites
{
    [BsonIgnoreExtraElements]
    public class Transaction
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("type")]
        public required string Type { get; set; }

        [BsonElement("amount")]
        public required double Amount { get; set; }

        [BsonElement("date")]
        public DateTime Date { get; set; }

        [BsonElement("isAcepted")]
        public bool IsAcepted { get; set; }

        [BsonElement("client")]
        public required Client Client { get; set; }

        [BsonElement("fund")]
        public required Fund Fund { get; set; }
    }
}
