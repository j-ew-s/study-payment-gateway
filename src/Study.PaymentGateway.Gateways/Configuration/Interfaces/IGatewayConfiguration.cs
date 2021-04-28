using System.Collections.Generic;

namespace Study.PaymentGateway.Gateways.Configuration.Interfaces
{
    public interface IGatewayConfiguration
    {
        public List<BankAPI> BankAPIs { get; set; }
    }
}