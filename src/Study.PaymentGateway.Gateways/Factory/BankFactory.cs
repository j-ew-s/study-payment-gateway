using Study.PaymentGateway.Domain.AcquiringBanksGateway;
using Study.PaymentGateway.Domain.AcquiringBanksGateway.Factory;
using Study.PaymentGateway.Domain.AcquiringBanksGateway.Services;
using Study.PaymentGateway.Domain.AcquiringBanksGateway.Services.GatewayConfig;
using Study.PaymentGateway.Gateways.Gateways;
using Study.PaymentGateway.Shared.Enums;

namespace Study.PaymentGateway.Gateways.Factory
{
    public class BankFactory : IBankFactory
    {
        public readonly IGatewayConfiguration gatewayConfiguration;
        public readonly IAPIExecutionService apiExecutionService;

        public BankFactory(IGatewayConfiguration gatewayConfiguration, IAPIExecutionService apiExecutionService)
        {
            this.gatewayConfiguration = gatewayConfiguration;
            this.apiExecutionService = apiExecutionService;
        }

        public IBankGateways GetInstance(BankCodeEnum bank)
        {
            switch (bank)
            {
                case BankCodeEnum.Visa:
                    return new VisaGateway(this.gatewayConfiguration, this.apiExecutionService);

                case BankCodeEnum.MasterCard:
                case BankCodeEnum.NotRecognised:
                default:
                    return null;
            }
        }
    }
}