using System;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Study.PaymentGateway.Domain.AcquiringBanksGateway.Services;
using Study.PaymentGateway.Domain.AcquiringBanksGateway.Services.GatewayConfig;
using Study.PaymentGateway.Domain.Entities.Banks;
using Study.PaymentGateway.Domain.Entities.Payments;
using Study.PaymentGateway.Gateways.Gateways;
using Study.PaymentGateway.Gateways.Tests.TestHelpers;
using Study.PaymentGateway.Gateways.Tests.TestHelpers.Data;
using Study.PaymentGateway.Shared.Enums;
using Xunit;

namespace Study.PaymentGateway.Gateways.Tests.Gateways
{
    public class VisaGatewayTest
    {
        private IGatewayConfiguration gatewayConfiguration;
        private Mock<IAPIExecutionService> mockApiExecutionService;
        private VisaGateway visaGateway;

        private const string token = "token";

        public VisaGatewayTest()
        {
            this.mockApiExecutionService = new Mock<IAPIExecutionService>();
        }

        [Fact]
        public async Task Login_When_ValidInput_Returns_ValidBankLoginResponse()
        {
            // Arrange
            var bankLoginResponse = new BankLoginResponse();
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
            var response = await this.visaGateway.Login();

            // Assert
            Assert.NotNull(response);
            Assert.Equal(token, this.visaGateway.Token);
            this.mockApiExecutionService.Verify(s => s.Post<BankLoginResponse>(loginActionUris.URI, It.IsAny<object>()), Times.Once);
        }

        [Fact]
        public async Task Login_When_InvalidInput_Returns_ValidBankLoginResponse()
        {
            // Arrange
            var bankLoginResponse = new BankLoginResponse();
            bankLoginResponse.Status = 400;
            bankLoginResponse.Body = null;
            bankLoginResponse.Message = "Invalid GatewayConfiguration";

            this.gatewayConfiguration = GatewayConfigurationDataHelper.GetGatewayConfiguration();

            this.mockApiExecutionService
                .Setup(s => s.Post<BankLoginResponse>(It.Is<string>(s => s == GatewayConfigurationDataHelper.loginURI), It.IsAny<object>()))
                .ReturnsAsync(bankLoginResponse);

            this.visaGateway = new VisaGateway(this.gatewayConfiguration, this.mockApiExecutionService.Object);

            var loginActionUris = GatewayConfigurationDataHelper
                .GetAllActionUris()
                .Where(w => w.Action == GatewayActionsEnum.Login).FirstOrDefault();

            // Act
            var response = await this.visaGateway.Login();

            // Assert
            Assert.NotNull(response);
            Assert.Null(this.visaGateway.Token);
            Assert.Equal(bankLoginResponse.Message, response.Message);
            Assert.Equal(bankLoginResponse.Body, response.Body);
            this.mockApiExecutionService.Verify(s => s.Post<BankLoginResponse>(loginActionUris.URI, It.IsAny<object>()), Times.Once);
        }

        [Fact]
        public async Task ProcessPayment_When_ValidInput_Returns_ValidBankResponse()
        {
            // Arrange
            var bankResponse = new BankResponse();
            bankResponse.Code = 00;
            bankResponse.PaymentID = Guid.NewGuid().ToString();

            var payment = PaymentBuilder.GetValidPayment();

            this.gatewayConfiguration = GatewayConfigurationDataHelper.GetGatewayConfiguration();

            this.mockApiExecutionService
                .Setup(s => s.Post<BankResponse>(It.Is<string>(s => s == GatewayConfigurationDataHelper.executePaymentURI), It.IsAny<object>()))
                .ReturnsAsync(bankResponse);

            this.visaGateway = new VisaGateway(this.gatewayConfiguration, this.mockApiExecutionService.Object);

            var paymentExecutionUris = GatewayConfigurationDataHelper
                .GetAllActionUris()
                .Where(w => w.Action == GatewayActionsEnum.ProcessPayment).FirstOrDefault();

            // Act
            var response = await this.visaGateway.ExecutesPayment(payment);

            // Assert
            Assert.NotNull(response);
            this.mockApiExecutionService.Verify(s => s.Post<BankResponse>(paymentExecutionUris.URI, It.IsAny<object>()), Times.Once);
        }
    }
}