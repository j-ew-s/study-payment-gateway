using AutoMapper;
using Study.PaymentGateway.Domain.Entities.Merchants;
using Study.PaymentGateway.Repository.MongoDB.Entities.Merchant;

namespace Study.PaymentGateway.Repository.MongoDB.Mapper.Merchants
{
    public class MerchantMongoMap : Profile
    {
        public MerchantMongoMap()
        {
            CreateMap<MerchantMongo, Merchant>().ReverseMap();
        }
    }
}