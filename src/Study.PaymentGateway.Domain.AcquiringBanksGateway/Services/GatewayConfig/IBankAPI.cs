using Study.PaymentGateway.Shared.Enums;

namespace Study.PaymentGateway.Domain.AcquiringBanksGateway.Services.GatewayConfig
{
    public interface IBankAPI
    {
        public string Name { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string Login { get; set; }
        public string ExecutePayment { get; set; }
        public string Domain { get; set; }
        public BankCodeEnum Code { get; set; }

        string GetFullPathExecution();
    }
}