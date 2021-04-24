using AutoMapper;
using Study.PaymentGateway.Domain.Entities.Payments;
using Study.PaymentGateway.Shared.DTO.Payments;

namespace Study.PaymentGateway.App.Mapper.Payments
{
    public class PaymentMap : Profile
    {
        public PaymentMap()
        {
            CreateMap<PaymentDTO, Payment>()
                .ForMember(d => d.Card, opt => opt.MapFrom(src => src.Card))
                .ForMember(d => d.Shopper, opt => opt.MapFrom(src => src.Shopper))
                .ReverseMap();
        }
    }
}