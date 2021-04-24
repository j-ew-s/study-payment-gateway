using System.Collections.Generic;

namespace Study.PaymentGateway.Gateways.Configuration.Interfaces
{
    public interface IGatewayConfiguration
    {
        public IList<BankAPI> BankAPIs { get; set; }
    }
}