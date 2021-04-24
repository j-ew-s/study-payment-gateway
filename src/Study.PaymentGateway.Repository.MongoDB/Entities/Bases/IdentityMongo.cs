using System;
using MongoDB.Bson.Serialization.Attributes;

namespace Study.PaymentGateway.Repository.MongoDB.Entities.Bases
{
    public class IdentityMongo
    {
        [BsonId]
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}