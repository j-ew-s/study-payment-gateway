using MongoDB.Driver;
using Study.PaymentGateway.Repository.MongoDB.Entities.Payment;

namespace Study.PaymentGateway.Repository.MongoDB.Configuration.Interfaces
{
    public interface IMongoDBConfiguration
    {
        public IMongoCollection<PaymentMongo> Payment { get; set; }
    }
}