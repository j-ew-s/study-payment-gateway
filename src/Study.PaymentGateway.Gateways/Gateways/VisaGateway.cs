using System.Linq;
using System.Threading.Tasks;
using Study.PaymentGateway.Domain.AcquiringBanksGateway;
using Study.PaymentGateway.Domain.AcquiringBanksGateway.Services;
using Study.PaymentGateway.Domain.AcquiringBanksGateway.Services.GatewayConfig;
using Study.PaymentGateway.Domain.Entities.Banks;
using Study.PaymentGateway.Domain.Entities.Payments;
using Study.PaymentGateway.Gateways.Models;
using Study.PaymentGateway.Shared.Enums;

namespace Study.PaymentGateway.Gateways.Gateways
{
    public class VisaGateway : BankGateways, IVisaGateway
    {
        public IBankAPI BankAPI { get; set; }
        public BankCodeEnum Bank => BankCodeEnum.Visa;

        public VisaGateway(IGatewayConfiguration gatewayConfiguration, IAPIExecutionService apiExecutionService)
            : base(gatewayConfiguration, apiExecutionService)
        {
            BankAPI = this.gatewayConfiguration.BankAPIs.Where(w => w.Code == BankCodeEnum.Visa).FirstOrDefault();
        }

        public override Task<BankResponse> ExecutesPayment(Payment payment)
        {
            var executePaymentPath = this.BankAPI.GetFullPathExecution();

            var executesPayment = new VisaExecutesPayment()
            {
                Token = this.Token,
                Payment = payment
            };

            return this.apiExecutionService.Post<BankResponse>(executePaymentPath, executesPayment);
        }

        public override async Task<BankLoginResponse> Login()
        {
            var loginPath = this.BankAPI.Login;

            var content = new
            {
                User = this.BankAPI.Login,
                Password = this.BankAPI.Password
            };

            var bankResponse = await this.apiExecutionService.Post<BankLoginResponse>(loginPath, content);
            this.Token = bankResponse.Body;

            return bankResponse;
        }
    }
}