using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace BTGIn_back.Entitites
{
    public class Client
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public required string Name {  get; set; }
        public required double Cash { get; set; }
        public List<Fund> Funds { get; set; } = [];
    }
}
