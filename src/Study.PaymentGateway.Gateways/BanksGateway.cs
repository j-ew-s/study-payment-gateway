using Study.PaymentGatewat.BanksGateway;

namespace Study.PaymentGateway.Gateways
{
    public class BanksGateway : IBanksGateway
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