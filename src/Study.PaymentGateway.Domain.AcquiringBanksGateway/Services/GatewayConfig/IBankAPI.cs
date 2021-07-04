using System.Collections.Generic;
using Study.PaymentGateway.Shared.Enums;

namespace Study.PaymentGateway.Domain.AcquiringBanksGateway.Services.GatewayConfig
{
    public interface IBankAPI
    {
        public string Name { get; set; }
        public BankCodeEnum Code { get; set; }
        public string Domain { get; set; }
        public List<IActionUris> ActionUris { get; set; }

        public IBankCredentials Credentials { get; set; }
    }
}