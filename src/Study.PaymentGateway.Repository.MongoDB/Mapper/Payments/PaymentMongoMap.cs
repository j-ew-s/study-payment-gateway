using AutoMapper;
using Study.PaymentGateway.Domain.Entities.Payments;
using Study.PaymentGateway.Repository.MongoDB.Entities.Payment;

namespace Study.PaymentGateway.Repository.MongoDB.Mapper.Payments
{
    public class PaymentMongoMap : Profile
    {
        public PaymentMongoMap()
        {
            CreateMap<PaymentMongo, Payment>()
                .ForMember(d => d.Card, opt => opt.MapFrom(src => src.Card))
                .ForMember(d => d.Shopper, opt => opt.MapFrom(src => src.Shopper))
                .ForMember(d => d.BankResponse, opt => opt.MapFrom(src => src.BankResponse))
                .ReverseMap();
        }
    }
}