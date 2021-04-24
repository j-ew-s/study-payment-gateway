namespace Study.PaymentGateway.Shared.DTO.Addresses
{
    public class AddressDTO
    {
        public string Street { get; set; }
        public string Complement { get; set; }
        public string ZIP { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }
}