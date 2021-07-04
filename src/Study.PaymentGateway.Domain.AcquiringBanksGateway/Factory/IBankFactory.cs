using Study.PaymentGateway.Shared.Enums;

namespace Study.PaymentGateway.Domain.AcquiringBanksGateway.Factory
{
    public interface IBankFactory
    {
        IBankGateways GetInstance(BankCodeEnum bank);
    }
}