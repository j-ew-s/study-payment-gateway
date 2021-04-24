using System.Linq;
using Study.PaymentGateway.Domain.AcquiringBanksGateway;
using Study.PaymentGateway.Gateways.Configuration;
using Study.PaymentGateway.Gateways.Configuration.Interfaces;
using Study.PaymentGateway.Gateways.Executor.Interface;
using Study.PaymentGateway.Shared.Enums;

namespace Study.PaymentGateway.Gateways
{
    public class VisaGateway : BankGateways, IVisaGateway
    {
        public BankAPI BankAPI { get; set; }

        public VisaGateway(IGatewayConfiguration gatewayConfiguration, IAPIExecutionService apiExecutionService)
            : base(gatewayConfiguration, apiExecutionService)
        {
        }

        public override void GetBankConfiguration()
        {
            BankAPI = this.gatewayConfiguration.BankAPIs.Where(w => w.Code == (int)BankCodeEnum.Visa).FirstOrDefault();
        }

        public override int ExecutesPayment(string URL, object shopperCard, object merchant)
        {
            throw new System.NotImplementedException();
        }

        public override void Login(string URL, string user, string pass)
        {
            throw new System.NotImplementedException();
        }
    }
}