using AutoMapper;
using Study.PaymentGateway.Repository.MongoDB.Configuration.Interfaces;

namespace Study.PaymentGateway.Repository.MongoDB.Repository
{
    public abstract class BaseRepository
    {
        public IMongoDBConfiguration mongoDBConfiguration;
        public readonly IMapper mapper;

        public BaseRepository(IMongoDBConfiguration mongoDBConfiguration, IMapper mapper)
        {
            this.mongoDBConfiguration = mongoDBConfiguration;
            this.mapper = mapper;
        }
    }
}