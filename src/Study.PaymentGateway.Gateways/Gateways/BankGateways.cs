using System.Threading.Tasks;
using Study.PaymentGateway.Domain.AcquiringBanksGateway.Services;
using Study.PaymentGateway.Domain.AcquiringBanksGateway.Services.GatewayConfig;
using Study.PaymentGateway.Domain.Entities.Banks;
using Study.PaymentGateway.Domain.Entities.Payments;

namespace Study.PaymentGateway.Gateways.Gateways
{
    public abstract class BankGateways
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

        public abstract Task<BankLoginResponse> Login();
    }
}