using System;
using AutoMapper;
using Study.PaymentGateway.Domain.Entities.Cards;
using Study.PaymentGateway.Repository.MongoDB.Entities.Card;

namespace Study.PaymentGateway.Repository.MongoDB.Mapper.Cards
{
    public class CardMongoMap : Profile
    {
        public CardMongoMap()
        {
            CreateMap<CardMongo, Card>();
            CreateMap<Card, CardMongo>()
                .ForMember(d => d.Number, opt => opt.MapFrom(src => GetLastFourDigits(src.Number)));
        }

        private int GetLastFourDigits(long number)
        {
            var n = number.ToString();
            return Convert.ToInt16(n.Substring(n.Length - 4, 4));
        }
    }
}