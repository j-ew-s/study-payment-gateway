using AutoMapper;
using Study.PaymentGateway.Domain.Entities.Addresses;
using Study.PaymentGateway.Repository.MongoDB.Entities.Address;

namespace Study.PaymentGateway.Repository.MongoDB.Mapper.Addresses
{
    public class AddressMongoMap : Profile
    {
        public AddressMongoMap()
        {
            CreateMap<AddressMongo, Address>().ReverseMap();
        }
    }
}