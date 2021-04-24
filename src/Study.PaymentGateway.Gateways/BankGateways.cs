using Study.PaymentGateway.Domain.AcquiringBanksGateway;

namespace Study.PaymentGateway.Gateways.Executor
{
    public class BankGateways : IBankGateways
    {
        public int ExecutesPayment(string URL, object shopperCard, object merchant)
        {
            throw new System.NotImplementedException();
        }

        public void SetupConnection(string URL, string user, string pass)
        {
            throw new System.NotImplementedException();
        }
    }
}