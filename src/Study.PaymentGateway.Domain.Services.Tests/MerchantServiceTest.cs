using System;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using Xunit;

namespace Study.PaymentGateway.Domain.Services.Tests
{
    public class MerchantServiceTest : BaseTest
    {
        public Fixture fixture = new Fixture();

        public MerchantService merchantService;

        private static Guid emptyGuid = Guid.Empty;
        private static Guid validGuid = Guid.NewGuid();

        public MerchantServiceTest()
        {
            this.merchantService = new MerchantService();
        }

        [Fact]
        public async Task Get_When_GuidIsValid_Returns_MerchantObject()
        {
            // Arrange

            // Act
            var response = await this.merchantService.Get(Guid.NewGuid());

            // Assert
            Assert.NotNull(response);
        }

        [Fact]
        public async Task Get_When_GuidIsInvalid_Returns_Null()
        {
            // Arrange

            // Act
            var response = await this.merchantService.Get(Guid.Empty);

            // Assert
            Assert.Null(response);
        }

        [Fact]
        public async Task Payment_When_InputsAreValid_Returns_PaymentObject()
        {
            // Arrange

            // Act
            var response = await this.merchantService.Payment(Guid.NewGuid(), Guid.NewGuid());

            // Assert
            Assert.NotNull(response);
        }

        [Theory]
        [InlineData("00000000-0000-0000-0000-000000000000", "00000000-0000-0000-0000-000000000000")]
        [InlineData("90d07dc0-1ae0-400b-8105-4a41dc0ac933", "00000000-0000-0000-0000-000000000000")]
        [InlineData("00000000-0000-0000-0000-000000000000", "90d07dc0-1ae0-400b-8105-4a41dc0ac933")]
        public async Task Payment_When_InputsMerchantIdInvalid_Returns_PaymentObject(string merchantId, string paymentId)
        {
            // Arrange
            var merchantGuid = Guid.Parse(merchantId);
            var paymentGuid = Guid.Parse(paymentId);

            // Act
            var response = await this.merchantService.Payment(merchantGuid, paymentGuid);

            // Assert
            Assert.Null(response);
        }

        [Fact]
        public async Task Payments_When_GuidIsValid_Returns_PaymentList()
        {
            // Arrange

            // Act
            var response = await this.merchantService.Payments(Guid.NewGuid());

            // Assert
            Assert.True(response.Any());
        }

        [Fact]
        public async Task Payments_When_GuidIsInvalid_Returns_EmptyList()
        {
            // Arrange

            // Act
            var response = await this.merchantService.Payments(Guid.Empty);

            // Assert
            Assert.Null(response);
        }
    }
}