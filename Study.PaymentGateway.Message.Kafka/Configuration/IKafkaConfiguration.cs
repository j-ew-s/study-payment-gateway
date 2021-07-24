using System.Collections.Generic;
using Study.PaymentGateway.Message.Kafka.Configuration.Producer;

namespace Study.PaymentGateway.Message.Kafka.Configuration
{
    public interface IKafkaConfiguration
    {
        public string[] Brokers { get; set; }
        public IDictionary<string, KafkaProducer> Producers { get; set; }
    }
}