using System;

namespace Study.PaymentGateway.Message.Kafka.Contracts.Events
{
    public class ProcessingPaymentExecutionSuccess
    {
        public Guid Id { get; set; }
        public decimal TotalCost { get; set; }
        public string Currency { get; set; }
        public string MerchandId { get; set; }
    }
}