using System.Collections.Generic;
using Study.PaymentGateway.Repository.MongoDB.Entities.Address;
using Study.PaymentGateway.Repository.MongoDB.Entities.Payment;

namespace Study.PaymentGateway.Repository.MongoDB.Entities.Merchant
{
    public class MerchantMongo
    {
        public MerchantMongo()
        {
            Address = new AddressMongo();
            Payments = new HashSet<PaymentMongo>();
        }

        public string Name { get; set; }
        public AddressMongo Address { get; set; }
        public IEnumerable<PaymentMongo> Payments { get; set; }
    }
}