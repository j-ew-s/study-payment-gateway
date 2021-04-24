using AutoMapper;
using Study.PaymentGateway.Domain.Entities.Addresses;
using Study.PaymentGateway.Shared.DTO.Addresses;

namespace Study.PaymentGateway.App.Mapper.Addresses
{
    public class AddressMap : Profile
    {
        public AddressMap()
        {
            CreateMap<Address, AddressDTO>().ReverseMap();
            CreateMap<AddressDTO, Address>().ReverseMap();
        }
    }
}