using System.Collections.Generic;
using Study.PaymentGateway.Gateways.Configuration.Interfaces;

namespace Study.PaymentGateway.Gateways.Configuration
{
    public class GatewayConfiguration : IGatewayConfiguration
    {
        public GatewayConfiguration()
        {
            this.BankAPIs = new List<BankAPI>();
        }

        public List<BankAPI> BankAPIs { get; set; }
    }
}