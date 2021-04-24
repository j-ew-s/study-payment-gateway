using AutoMapper;
using Study.PaymentGateway.Domain.Entities.Clients;
using Study.PaymentGateway.Repository.MongoDB.Entities.Clients;

namespace Study.PaymentGateway.Repository.MongoDB.Mapper.Clients
{
    public class ShoppersMongoMap : Profile
    {
        public ShoppersMongoMap()
        {
            CreateMap<ShopperMongo, Shopper>().ReverseMap();
        }
    }
}