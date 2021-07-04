using System.Collections.Generic;

namespace Study.PaymentGateway.Domain.AcquiringBanksGateway.Services.GatewayConfig
{
    public interface IGatewayConfiguration
    {
        public List<IBankAPI> BankAPIs { get; set; }
    }
}