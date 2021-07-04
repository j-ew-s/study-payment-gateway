using System.Collections.Generic;
using Study.PaymentGateway.Domain.AcquiringBanksGateway.Services.GatewayConfig;

namespace Study.PaymentGateway.Gateways.Configuration
{
    public class GatewayConfiguration : IGatewayConfiguration
    {
        public GatewayConfiguration()
        {
            this.BankAPIs = new List<IBankAPI>();
        }

        public List<IBankAPI> BankAPIs { get; set; }
    }
}