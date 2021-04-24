using System.Collections.Generic;
using Study.PaymentGateway.Shared.Enums;

namespace Study.PaymentGateway.Gateways.Configuration.Interfaces
{
    public interface IBankAPI
    {
        public string Name { get; set; }
        public BankCodeEnum Code { get; set; }
        public string Domain { get; set; }
        public List<ActionUris> ActionUris { get; set; }
    }
}