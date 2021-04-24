namespace Study.PaymentGateway.Repository.MongoDB.Entities.Address
{
    public class AddressMongo
    {
        public string Street { get; set; }
        public string Complement { get; set; }
        public string ZIP { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }
}