using System.Linq;
using System.Threading.Tasks;
using Study.PaymentGateway.Domain.AcquiringBanksGateway;
using Study.PaymentGateway.Domain.Entities.Banks;
using Study.PaymentGateway.Domain.Entities.Payments;
using Study.PaymentGateway.Gateways.Configuration;
using Study.PaymentGateway.Gateways.Configuration.Interfaces;
using Study.PaymentGateway.Gateways.Executor.Interface;
using Study.PaymentGateway.Gateways.Models;
using Study.PaymentGateway.Shared.Enums;

namespace Study.PaymentGateway.Gateways
{
    public class VisaGateway : BankGateways, IVisaGateway
    {
        public BankAPI BankAPI { get; set; }

        public VisaGateway(IGatewayConfiguration gatewayConfiguration, IAPIExecutionService apiExecutionService)
            : base(gatewayConfiguration, apiExecutionService)
        {
            BankAPI = this.gatewayConfiguration.BankAPIs.Where(w => w.Code == BankCodeEnum.Visa).FirstOrDefault();
        }

        public override async Task<BankResponse> ExecutesPayment(Payment payment)
        {
            var executesPaymentConfig = this.BankAPI.ActionUris.Where(w => w.Action == GatewayActionsEnum.ProcessPayment).FirstOrDefault();

            var executesPayment = new VisaExecutesPayment()
            {
                Token = this.Token,
                Payment = payment
            };

            return await this.apiExecutionService.Post<BankResponse>(executesPaymentConfig.URI, executesPayment);
        }

        public override async Task<BankLoginResponse> Login()
        {
            var loginConfig = this.BankAPI.ActionUris.Where(w => w.Action == GatewayActionsEnum.Login).FirstOrDefault();
            var bankResponse = new BankLoginResponse();

            if (IsValid(loginConfig))
            {
                bankResponse = await this.apiExecutionService.Post<BankLoginResponse>(loginConfig.URI, BankAPI.Credentials);
                this.Token = bankResponse.Body;
            }
            else
            {
                bankResponse.Body = null;
                bankResponse.Status = 400;
                bankResponse.Message = "Invalid GatewayConfiguration";
            }

            return bankResponse;
        }

        private bool IsValid(ActionUris loginConfig)
        {
            return loginConfig != null ||
                !string.IsNullOrWhiteSpace(loginConfig.URI) ||
                !string.IsNullOrWhiteSpace(loginConfig.HttpVerb) ||
                BankAPI.Credentials != null ||
                !string.IsNullOrWhiteSpace(BankAPI.Credentials.User) ||
                !string.IsNullOrWhiteSpace(BankAPI.Credentials.Password);
        }
    }
}