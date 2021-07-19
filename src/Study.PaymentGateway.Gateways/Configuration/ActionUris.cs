using Study.PaymentGateway.Domain.AcquiringBanksGateway.Services.GatewayConfig;
using Study.PaymentGateway.Shared.Enums;

namespace Study.PaymentGateway.Gateways.Configuration
{
    public class ActionUris : IActionUris
    {
        public string HttpVerb { get; set; }
        public string URI { get; set; }
        public GatewayActionsEnum Action { get; set; }
    }
}