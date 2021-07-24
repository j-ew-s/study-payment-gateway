namespace Study.PaymentGateway.Message.Kafka.Configuration.Producer
{
    public class KafkaProducer
    {
        public string Topic { get; set; }
        public KafkaFlow.Acks Acks { get; set; }
    }
}