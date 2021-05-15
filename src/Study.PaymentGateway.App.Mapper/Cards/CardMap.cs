using System;
using AutoMapper;
using Study.PaymentGateway.Domain.Entities.Cards;
using Study.PaymentGateway.Shared.DTO.Cards;

namespace Study.PaymentGateway.App.Mapper.Cards
{
    public class CardMap : Profile
    {
        public CardMap()
        {
            CreateMap<CardDTO, Card>().ReverseMap();
            CreateMap<CardResponseDTO, Card>();
            CreateMap<Card, CardResponseDTO>()
                .ForMember(
                    destination => destination.Number,
                    options => options.MapFrom(
                        source => GetLastFourDigits(source.Number)
                    ));
        }

        private int GetLastFourDigits(long cardNumber)
        {
            var parsedCardNumber = cardNumber.ToString();
            var lastFourDigits = parsedCardNumber.Substring((parsedCardNumber.Length - 4), 4);
            return Convert.ToInt32(lastFourDigits);
        }
    }
}