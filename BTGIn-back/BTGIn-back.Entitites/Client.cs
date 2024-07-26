using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace BTGIn_back.Entitites
{
    [BsonIgnoreExtraElements]
    public class Client
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("name")]
        public required string Name {  get; set; }

        [BsonElement("identification")]
        public required int Identification {  get; set; }

        [BsonElement("cash")]
        public required double Cash { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement("funds")]
        public List<Fund>? Funds { get; set; } = [];
    }
}
