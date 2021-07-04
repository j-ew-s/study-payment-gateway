namespace Study.PaymentGateway.Domain.AcquiringBanksGateway.Services.GatewayConfig
{
    public interface IBankCredentials
    {
        public string User { get; set; }
        public string Password { get; set; }
    }
}