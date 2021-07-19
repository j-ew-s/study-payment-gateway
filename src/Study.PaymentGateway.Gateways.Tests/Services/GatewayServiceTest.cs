using Moq;
using Study.PaymentGateway.Domain.AcquiringBanksGateway;
using Study.PaymentGateway.Domain.AcquiringBanksGateway.Factory;
using Study.PaymentGateway.Domain.AcquiringBanksGateway.Services;
using Study.PaymentGateway.Domain.AcquiringBanksGateway.Services.GatewayConfig;
using Study.PaymentGateway.Domain.Entities.Banks;
using Study.PaymentGateway.Domain.Entities.Payments;
using Study.PaymentGateway.Gateways.Executor;
using Study.PaymentGateway.Gateways.Gateways;
using Study.PaymentGateway.Gateways.Services;
using Study.PaymentGateway.Gateways.Tests.TestHelpers.Data;
using Study.PaymentGateway.Shared.Enums;
using Xunit;

namespace Study.PaymentGateway.Gateways.Tests.Services
{
    public class GatewayServiceTest
    {
        public Mock<IGatewayConfiguration> mockGatewayConfiguration;
        public Mock<IAPIExecutionService> mockApiExecutionService;
        public Mock<IBankFactory> mockBankFactory;
        public Mock<IBankGateways> mockBankGateways;

        public GatewayService gateway;

        public GatewayServiceTest()
        {
            this.mockGatewayConfiguration = new Mock<IGatewayConfiguration>();
            this.mockApiExecutionService = new Mock<IAPIExecutionService>();
            this.mockBankFactory = new Mock<IBankFactory>();
            this.mockBankGateways = new Mock<IBankGateways>();

            this.gateway = new GatewayService(this.mockGatewayConfiguration.Object, this.mockApiExecutionService.Object, this.mockBankFactory.Object);
        }

        [Fact]
        public void ExecutesPayment_WhenPaymentIsValid_ShuldReturnValidBankResponse()
        {
            // Arrange
            this.mockBankFactory.Setup(s => s.GetInstance(It.Is<BankCodeEnum>(x => x == BankCodeEnum.Visa))).Returns(It.IsAny<IBankGateways>());
            this.mockBankGateways.Setup(s => s.ExecutesPayment(It.IsAny<Payment>())).ReturnsAsync(It.IsAny<BankResponse>());
            var payment = PaymentBuilder.GetValidPayment();

            // Act
            this.gateway.ExecutesPayment(payment);

            // Assert
            this.mockBankFactory.Verify(s => s.GetInstance(BankCodeEnum.Visa), Times.Once);
            this.mockBankGateways.Verify(s => s.ExecutesPayment(It.IsAny<Payment>()), Times.Once);
        }
    }
}