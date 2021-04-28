using Study.PaymentGateway.Shared.Enums;

namespace Study.PaymentGateway.Gateways.Configuration.Interfaces
{
    public interface IActionUris
    {
        public GatewayActionsEnum Action { get; set; }
        public string HttpVerb { get; set; }
        public string URI { get; set; }
    }
}