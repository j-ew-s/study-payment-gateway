using AutoMapper;
using Study.PaymentGateway.Domain.Entities.Bases;
using Study.PaymentGateway.Repository.MongoDB.Entities.Bases;

namespace Study.PaymentGateway.Repository.MongoDB.Mapper.Bases
{
    public class IdentityMongoMap : Profile
    {
        public IdentityMongoMap()
        {
            CreateMap<Identity, IdentityMongo>().ReverseMap();
        }
    }
}