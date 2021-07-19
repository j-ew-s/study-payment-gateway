using System.Threading.Tasks;
using Study.PaymentGateway.Domain.AcquiringBanksGateway.Services.GatewayConfig;
using Study.PaymentGateway.Domain.Entities.Banks;
using Study.PaymentGateway.Domain.Entities.Payments;
using Study.PaymentGateway.Shared.Enums;

namespace Study.PaymentGateway.Domain.AcquiringBanksGateway.Services
{
    public interface IGatewayServices
    {
        public abstract IGatewayConfiguration gatewayConfiguration { get; set; }
        public abstract IAPIExecutionService apiExecutionService { get; set; }

        public IBankGateways BankGateway { get; set; }

        Task<BankResponse> ExecutesPayment(Payment payment);

        Task<BankLoginResponse> Login();
    }
}