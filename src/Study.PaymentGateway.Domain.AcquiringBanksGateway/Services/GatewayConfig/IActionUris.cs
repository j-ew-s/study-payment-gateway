using Study.PaymentGateway.Shared.Enums;

namespace Study.PaymentGateway.Domain.AcquiringBanksGateway.Services.GatewayConfig
{
    public interface IActionUris
    {
        public GatewayActionsEnum Action { get; set; }
        public string HttpVerb { get; set; }
        public string URI { get; set; }
    }
}