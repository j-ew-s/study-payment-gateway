using Study.PaymentGateway.Domain.Entities.Payments;

namespace Study.PaymentGateway.Gateways.Models
{
    internal class VisaExecutesPayment
    {
        public string Token { get; set; }
        public Payment Payment { get; set; }
    }
}