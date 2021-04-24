using Study.PaymentGateway.Repository.MongoDB.Configuration.Settings.Interfaces;

namespace Study.PaymentGateway.Repository.MongoDB.Configuration.Settings
{
    public class MongoDBSettings : IMongoDBSettings
    {
        public string ConnectionString { get; set; }
        public string Database { get; set; }
        public string Collection { get; set; }
    }
}