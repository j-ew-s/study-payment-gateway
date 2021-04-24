using AutoMapper;
using Study.PaymentGateway.Domain.Entities.Clients;
using Study.PaymentGateway.Shared.DTO.Clients;

namespace Study.PaymentGateway.App.Mapper.Clients
{
    public class ShoppersMap : Profile
    {
        public ShoppersMap()
        {
            CreateMap<Shopper, ShopperDTO>().ReverseMap();
            CreateMap<ShopperDTO, Shopper>().ReverseMap();
        }
    }
}