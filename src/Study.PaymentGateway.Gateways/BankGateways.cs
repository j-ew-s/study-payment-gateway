using System.Threading.Tasks;
using Study.PaymentGateway.Domain.AcquiringBanksGateway;
using Study.PaymentGateway.Domain.Entities.Banks;
using Study.PaymentGateway.Domain.Entities.Payments;
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

        public abstract Task<BankResponse> ExecutesPayment(Payment payment);

        public abstract Task<BankLoginResponse> Login(string user, string pass);

        public abstract void GetBankConfiguration();
    }
}