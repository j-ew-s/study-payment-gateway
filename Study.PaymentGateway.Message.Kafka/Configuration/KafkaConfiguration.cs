using System.Collections.Generic;
using Study.PaymentGateway.Message.Kafka.Configuration.Producer;

namespace Study.PaymentGateway.Message.Kafka.Configuration
{
    public class KafkaConfiguration : IKafkaConfiguration
    {
        public string[] Brokers { get; set; }
        public IDictionary<string, KafkaProducer> Producers { get; set; }
    }
}