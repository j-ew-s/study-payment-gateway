using System.Collections.Generic;
using Study.PaymentGateway.Domain.AcquiringBanksGateway.Services.GatewayConfig;
using Study.PaymentGateway.Shared.Enums;

namespace Study.PaymentGateway.Gateways.Configuration
{
    public class BankAPI : IBankAPI
    {
        public BankAPI()
        {
            this.ActionUris = new List<IActionUris>();
        }

        public string Institution { get; set; }
        public string Domain { get; set; }
        public List<IActionUris> ActionUris { get; set; }
        public string Name { get; set; }
        public BankCodeEnum Code { get; set; }
        public IBankCredentials Credentials { get; set; }
    }
}