using AutoMapper;
using Study.PaymentGateway.App.Mapper.Addresses;
using Study.PaymentGateway.App.Mapper.Bases;
using Study.PaymentGateway.App.Mapper.Cards;
using Study.PaymentGateway.App.Mapper.Clients;
using Study.PaymentGateway.App.Mapper.Merchants;
using Study.PaymentGateway.App.Mapper.Paging;
using Study.PaymentGateway.App.Mapper.Payments;
using Study.PaymentGateway.Repository.MongoDB.Mapper.Addresses;
using Study.PaymentGateway.Repository.MongoDB.Mapper.BankResponses;
using Study.PaymentGateway.Repository.MongoDB.Mapper.Bases;
using Study.PaymentGateway.Repository.MongoDB.Mapper.Cards;
using Study.PaymentGateway.Repository.MongoDB.Mapper.Clients;
using Study.PaymentGateway.Repository.MongoDB.Mapper.Merchants;
using Study.PaymentGateway.Repository.MongoDB.Mapper.Payments;

namespace Study.PaymentGateway.API.Configurations
{
    public class AutoMapperConfiguration
    {
        public IMapper Mapper { get; set; }

        public AutoMapperConfiguration()
        {
            var mappingConfig = new AutoMapper.MapperConfiguration(mc =>
            {
                mc.AddProfile(new PaymentMap());
                mc.AddProfile(new CardMap());
                mc.AddProfile(new AddressMap());
                mc.AddProfile(new ShoppersMap());
                mc.AddProfile(new MerchantMap());
                mc.AddProfile(new IdentityMap());

                mc.AddProfile(new PaymentMongoMap());
                mc.AddProfile(new BankResponseMongoMap());
                mc.AddProfile(new CardMongoMap());
                mc.AddProfile(new AddressMongoMap());
                mc.AddProfile(new ShoppersMongoMap());
                mc.AddProfile(new MerchantMongoMap());
                mc.AddProfile(new IdentityMongoMap());

                mc.AddProfile(new PagedResultMap());
            });

            Mapper = mappingConfig.CreateMapper();
        }
    }
}