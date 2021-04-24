using AutoMapper;
using Study.PaymentGateway.Domain.Entities.Bases;
using Study.PaymentGateway.Shared.DTO.Bases;

namespace Study.PaymentGateway.App.Mapper.Bases
{
    public class IdentityMap : Profile
    {
        public IdentityMap()
        {
            CreateMap<Identity, IdentityDTO>().ReverseMap();
            CreateMap<IdentityDTO, Identity>().ReverseMap();
        }
    }
}