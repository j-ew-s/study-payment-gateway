using Study.PaymentGateway.Repository.MongoDB.Entities.Address;

namespace Study.PaymentGateway.Repository.MongoDB.Entities.Clients
{
    public class ShopperMongo
    {
        public ShopperMongo()
        {
            Address = new AddressMongo();
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public AddressMongo Address { get; set; }
    }
}