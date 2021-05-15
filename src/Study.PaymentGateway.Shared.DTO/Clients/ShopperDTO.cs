using Study.PaymentGateway.Shared.DTO.Addresses;
using Study.PaymentGateway.Shared.DTO.Bases;

namespace Study.PaymentGateway.Shared.DTO.Clients
{
    public class ShopperDTO
    {
        public ShopperDTO()
        {
            Address = new AddressDTO();
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public AddressDTO Address { get; set; }
    }
}