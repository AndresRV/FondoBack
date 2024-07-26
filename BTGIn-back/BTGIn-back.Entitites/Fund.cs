using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace BTGIn_back.Entitites
{
    public class Fund
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Name {  get; set; }
        public double MinimumRegistrationAmount { get; set; }
        public string Category { get; set; }
        [BsonIgnoreIfNull]
        public double InitialCapital { get; set; }
    }
}
