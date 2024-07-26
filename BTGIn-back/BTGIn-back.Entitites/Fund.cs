using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace BTGIn_back.Entitites
{
    [BsonIgnoreExtraElements]
    public class Fund
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("name")]
        public string Name {  get; set; }

        [BsonElement("minimumRegistrationAmount")]
        public double MinimumRegistrationAmount { get; set; }

        [BsonElement("category")]
        public string Category { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement("inscriptionCapital")]
        public double? InscriptionCapital { get; set; }
    }
}
