using Study.PaymentGateway.Domain.AcquiringBanksGateway;
using Study.PaymentGateway.Gateways.Configuration;
using Study.PaymentGateway.Gateways.Configuration.Interfaces;
using Study.PaymentGateway.Gateways.Executor.Interface;

namespace Study.PaymentGateway.Gateways
{
    public abstract class BankGateways : IBankGateways
    {
        public readonly IGatewayConfiguration gatewayConfiguration;
        public readonly IAPIExecutionService apiExecutionService;

        public BankGateways(IGatewayConfiguration gatewayConfiguration, IAPIExecutionService apiExecutionService)
        {
            this.gatewayConfiguration = gatewayConfiguration;
            this.apiExecutionService = apiExecutionService;
        }

        public string Token { get; set; }

        public abstract int ExecutesPayment(string URL, object shopperCard, object merchant);

        public abstract void Login(string URL, string user, string pass);

        public abstract void GetBankConfiguration();
    }
}