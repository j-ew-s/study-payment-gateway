using System.Collections.Generic;
using Study.PaymentGateway.Gateways.Configuration.Interfaces;

namespace Study.PaymentGateway.Gateways.Configuration
{
    public class GatewayConfiguration : IGatewayConfiguration
    {
        public IList<IBankAPI> BankAPIs { get; set; }
    }
}