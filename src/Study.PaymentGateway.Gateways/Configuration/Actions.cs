namespace Study.PaymentGateway.Gateways.Configuration
{
    using Study.PaymentGateway.Domain.AcquiringBanksGateway.Services.GatewayConfig;

    public class Actions : IActions
    {
        public object Login { get; set; }
        public object ExecutePayment { get; set; }
    }
}