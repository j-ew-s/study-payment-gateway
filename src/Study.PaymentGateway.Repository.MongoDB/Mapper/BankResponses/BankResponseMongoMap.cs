using AutoMapper;
using Study.PaymentGateway.Domain.Entities.Banks;
using Study.PaymentGateway.Repository.MongoDB.Entities.Banks;

namespace Study.PaymentGateway.Repository.MongoDB.Mapper.BankResponses
{
    public class BankResponseMongoMap : Profile
    {
        public BankResponseMongoMap()
        {
            CreateMap<BankResponseMongo, BankResponse>().ReverseMap();
        }
    }
}