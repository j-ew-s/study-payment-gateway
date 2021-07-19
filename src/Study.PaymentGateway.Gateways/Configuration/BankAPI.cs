using Study.PaymentGateway.Domain.AcquiringBanksGateway.Services.GatewayConfig;
using Study.PaymentGateway.Shared.Enums;

namespace Study.PaymentGateway.Gateways.Configuration
{
    public class BankAPI : IBankAPI
    {
        public BankAPI()
        {
        }

        public string Institution { get; set; }
        public string Domain { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string ExecutePayment { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public BankCodeEnum Code { get; set; }

        public string GetFullPathExecution()
        {
            return this.Domain + this.ExecutePayment;
        }
    }
}