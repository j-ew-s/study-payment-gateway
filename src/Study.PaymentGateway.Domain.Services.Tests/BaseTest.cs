namespace Study.PaymentGateway.Domain.Services.Tests
{
    using AutoMapper;
    using Moq;

    public abstract class BaseTest
    {
        public Mock<IMapper> mockMapper;

        public BaseTest()
        {
            this.mockMapper = new Mock<IMapper>();
        }
    }
}