using MongoDB.Driver;
using Study.PaymentGateway.Repository.MongoDB.Configuration.Interfaces;
using Study.PaymentGateway.Repository.MongoDB.Configuration.Settings.Interfaces;
using Study.PaymentGateway.Repository.MongoDB.Entities.Payment;

namespace Study.PaymentGateway.Repository.MongoDB.Configuration
{
    public class MongoDBConfiguration : IMongoDBConfiguration
    {
        private IMongoDBSettings mongoDBSettings;
        public IMongoDatabase DB;
        public IMongoCollection<PaymentMongo> Payment { get; set; }

        public MongoDBConfiguration(IMongoDBSettings mongoDBSettings)
        {
            this.mongoDBSettings = mongoDBSettings;
            this.Configure();
        }

        private void Configure()
        {
            var cluster = new MongoClient(this.mongoDBSettings.ConnectionString);
            DB = cluster.GetDatabase(this.mongoDBSettings.Database);

            this.GenerateCollectionName();
        }

        private void GenerateCollectionName()
        {
            this.Payment = DB.GetCollection<PaymentMongo>("Payments");
        }
    }
}