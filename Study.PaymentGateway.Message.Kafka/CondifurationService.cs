using KafkaFlow;
using KafkaFlow.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Study.PaymentGateway.Message.Kafka.Configuration;
using Study.PaymentGateway.Message.Kafka.Configuration.Producer;
using Study.PaymentGateway.Message.Kafka.Contracts.Events;

namespace Study.PaymentGateway.Message.Kafka
{
    public static class CondifurationService
    {
        public static void SetKafkaService(IServiceCollection services, IKafkaConfiguration kafkaConfiguration)
        {
            kafkaConfiguration.Producers.TryGetValue(KafkaConstants.Producers.PaymentExecutionSuccess, out var paymentExecutionSuccess);

            services.AddKafka(kafka => kafka
                .AddCluster(cluster => cluster
                    .WithBrokers(kafkaConfiguration.Brokers)
                    .CreateProducer<ProcessingPaymentExecutionSuccess>(paymentExecutionSuccess)
                )
            );
        }

        private static IClusterConfigurationBuilder CreateProducer<IProcuder>(
             this IClusterConfigurationBuilder clusterConfigurationBuilder,
             KafkaProducer kafkaproducer) where IProcuder : class
        {
            clusterConfigurationBuilder
                    .AddProducer<IProcuder>(
                        producer => producer
                            .DefaultTopic(kafkaproducer.Topic)
                            .WithAcks(kafkaproducer.Acks)
                    );

            return clusterConfigurationBuilder;
        }
    }
}