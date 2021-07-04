using Study.PaymentGateway.Domain.AcquiringBanksGateway.Services.GatewayConfig;

namespace Study.PaymentGateway.Gateways.Configuration
{
    public class BankCredentials : IBankCredentials
    {
        public string User { get; set; }
        public string Password { get; set; }
    }
}