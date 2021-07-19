using System.Threading.Tasks;
using Study.PaymentGateway.Domain.AcquiringBanksGateway;
using Study.PaymentGateway.Domain.AcquiringBanksGateway.Factory;
using Study.PaymentGateway.Domain.AcquiringBanksGateway.Services;
using Study.PaymentGateway.Domain.AcquiringBanksGateway.Services.GatewayConfig;
using Study.PaymentGateway.Domain.Entities.Banks;
using Study.PaymentGateway.Domain.Entities.Payments;
using Study.PaymentGateway.Shared.Enums;

namespace Study.PaymentGateway.Gateways.Services
{
    public class GatewayService : IGatewayServices
    {
        public IGatewayConfiguration gatewayConfiguration { get; set; }
        public IAPIExecutionService apiExecutionService { get; set; }
        public IBankGateways BankGateway { get; set; }

        private readonly IBankFactory bankFactory;

        public GatewayService(IGatewayConfiguration gatewayConfiguration, IAPIExecutionService apiExecutionService, IBankFactory bankFactory)
        {
            this.gatewayConfiguration = gatewayConfiguration;
            this.apiExecutionService = apiExecutionService;
            this.bankFactory = bankFactory;
        }

        public Task<BankResponse> ExecutesPayment(Payment payment)
        {
            SetBankGatewayInstance(payment.Card.Bank());

            return this.BankGateway.ExecutesPayment(payment);
        }

        public Task<BankLoginResponse> Login()
        {
            throw new System.NotImplementedException();
        }

        private void SetBankGatewayInstance(BankCodeEnum bank)
        {
            BankGateway = bankFactory.GetInstance(bank);
        }
    }
}