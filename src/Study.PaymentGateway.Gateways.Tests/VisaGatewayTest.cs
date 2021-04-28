using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using Moq;
using Study.PaymentGateway.Domain.Entities.Banks;
using Study.PaymentGateway.Gateways.Configuration;
using Study.PaymentGateway.Gateways.Configuration.Interfaces;
using Study.PaymentGateway.Gateways.Executor.Interface;
using Study.PaymentGateway.Gateways.Tests.TestHelpers;
using Study.PaymentGateway.Shared.Enums;
using Xunit;

namespace Study.PaymentGateway.Gateways.Tests
{
    public class VisaGatewayTest
    {
        private Fixture fixture = new Fixture();
        private IGatewayConfiguration gatewayConfiguration;
        private Mock<IAPIExecutionService> mockApiExecutionService;
        private VisaGateway visaGateway;

        private const string user = "user";
        private const string password = "pass";
        private const string token = "token";

        public VisaGatewayTest()
        {
            this.mockApiExecutionService = new Mock<IAPIExecutionService>();
        }

        [Fact]
        public async Task Login_When_ValidInput_Returns_ValidBankLoginResponse()
        {
            // Arrange

            var bankLoginResponse = this.fixture.Create<BankLoginResponse>();
            bankLoginResponse.Status = 200;
            bankLoginResponse.Body = token;

            this.gatewayConfiguration = GatewayConfigurationDataHelper.GetGatewayConfiguration();

            this.mockApiExecutionService
                .Setup(s => s.Post<BankLoginResponse>(It.Is<string>(s => s == GatewayConfigurationDataHelper.loginURI), It.IsAny<object>()))
                .ReturnsAsync(bankLoginResponse);

            this.visaGateway = new VisaGateway(this.gatewayConfiguration, this.mockApiExecutionService.Object);

            var loginActionUris = GatewayConfigurationDataHelper
                .GetAllActionUris()
                .Where(w => w.Action == GatewayActionsEnum.Login).FirstOrDefault();

            // Act
            var response = await this.visaGateway.Login(user, password);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(token, this.visaGateway.Token);
            this.mockApiExecutionService.Verify(s => s.Post<BankLoginResponse>(loginActionUris.URI, It.IsAny<object>()), Times.Once);
        }
    }
}