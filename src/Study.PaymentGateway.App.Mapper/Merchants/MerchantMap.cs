using AutoMapper;
using Study.PaymentGateway.Domain.Entities.Merchants;
using Study.PaymentGateway.Shared.DTO.Merchants;

namespace Study.PaymentGateway.App.Mapper.Merchants
{
    public class MerchantMap : Profile
    {
        public MerchantMap()
        {
            CreateMap<Merchant, MerchantDTO>().ReverseMap();
            CreateMap<MerchantDTO, Merchant>().ReverseMap();
        }
    }
}