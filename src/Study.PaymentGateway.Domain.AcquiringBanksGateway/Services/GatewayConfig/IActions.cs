namespace Study.PaymentGateway.Domain.AcquiringBanksGateway.Services.GatewayConfig
{
    public interface IActions
    {
        public object Login { get; set; }
        public object ExecutePayment { get; set; }
    }
}