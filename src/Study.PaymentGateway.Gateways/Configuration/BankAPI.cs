using System.Collections.Generic;
using Study.PaymentGateway.Gateways.Configuration.Interfaces;
using Study.PaymentGateway.Shared.Enums;

namespace Study.PaymentGateway.Gateways.Configuration
{
    public class BankAPI : IBankAPI
    {
        public BankAPI()
        {
            this.ActionUris = new List<ActionUris>();
        }

        public string Institution { get; set; }
        public string Domain { get; set; }
        public List<ActionUris> ActionUris { get; set; }
        public string Name { get; set; }
        public int Code { get; set; }
        BankCodeEnum IBankAPI.Code { get; set; }
    }
}