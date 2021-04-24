using Study.PaymentGateway.Shared.DTO.Addresses;
using Study.PaymentGateway.Shared.DTO.Bases;

namespace Study.PaymentGateway.Shared.DTO.Merchants
{
    public class MerchantDTO : IdentityDTO
    {
        public MerchantDTO()
        {
            Address = new AddressDTO();
        }

        public string Name { get; set; }
        public AddressDTO Address { get; set; }
    }
}