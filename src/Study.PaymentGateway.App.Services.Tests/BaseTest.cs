using AutoMapper;
using Moq;
using Study.PaymentGateway.API.Configurations;

namespace Study.PaymentGateway.App.Services.Tests
{
    public abstract class BaseTest
    {
        public BaseTest()
        {
            var autoMapperConfiguration = new AutoMapperConfiguration();
            this.Map = autoMapperConfiguration.Mapper;
            this.mockMapper = new Mock<IMapper>();
        }

        public IMapper Map { get; }

        public Mock<IMapper> mockMapper;
    }
}