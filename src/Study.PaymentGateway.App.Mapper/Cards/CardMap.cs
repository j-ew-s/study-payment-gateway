using AutoMapper;
using Study.PaymentGateway.Domain.Entities.Cards;
using Study.PaymentGateway.Shared.DTO.Cards;

namespace Study.PaymentGateway.App.Mapper.Cards
{
    public class CardMap : Profile
    {
        public CardMap()
        {
            CreateMap<Card, CardDTO>().ReverseMap();
            CreateMap<CardDTO, Card>().ReverseMap();
        }
    }
}