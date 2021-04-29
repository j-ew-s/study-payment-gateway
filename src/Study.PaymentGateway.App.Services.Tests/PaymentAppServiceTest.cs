using System.Threading.Tasks;
using AutoFixture;
using Moq;
using Study.PaymentGateway.App.Services.Tests.Util.DataGenerate;
using Study.PaymentGateway.Domain.Entities.Payments;
using Study.PaymentGateway.Domain.Services.Interfaces;
using Study.PaymentGateway.Shared.DTO.HTTPResponses;
using Study.PaymentGateway.Shared.DTO.Payments;
using Xunit;

namespace Study.PaymentGateway.App.Services.Tests
{
    public class PaymentAppServiceTest : BaseTest
    {
        private Fixture fixture = new Fixture();
        private Mock<IPaymentService> mockPaymentService;
        private PaymentAppService paymentAppService;

        public PaymentAppServiceTest()
        {
            this.mockPaymentService = new Mock<IPaymentService>();

            this.paymentAppService = new PaymentAppService(this.mockPaymentService.Object, this.Map);
        }

        [Fact]
        public async Task ProcessPayment_WhenValidPayment_ReturnHttpResponseStatus200()
        {
            // Arrange
            var paymentDTO = PaymentGenerate.GetValidPaymentDTO();

            var payment = PaymentGenerate.GetValidPayment();

            var expected = new HttpResponseDTO<PaymentDTO>();
            expected.Status = 201;
            expected.ErrorMessages = new System.Collections.Generic.List<string>();
            expected.Response = paymentDTO;

            this.mockPaymentService
                .Setup(s => s.ProcessPaymentAsync(It.IsAny<Payment>()))
                .ReturnsAsync(payment);

            this.mockMapper
                .Setup(x => x.Map<Payment>(It.IsAny<PaymentDTO>()))
                .Returns(payment);

            this.mockMapper
                .Setup(x => x.Map<PaymentDTO>(It.IsAny<Payment>()))
                .Returns(paymentDTO);

            // Act
            var response = await paymentAppService.ProcessPaymentAsync(paymentDTO);

            // Assert
            Assert.Equal(expected.Status, response.Status);
            Assert.Equal(expected.ErrorMessages, response.ErrorMessages);
        }

        [Fact]
        public async Task ProcessPayment_WhenInvalidValidPayment_ReturnHttpResponseStatus400()
        {
            // Arrange
            var paymentDto = fixture.Create<PaymentDTO>();
            paymentDto.Card = new Shared.DTO.Cards.CardDTO();

            var payment = fixture.Create<Payment>();
            payment.Card = new Domain.Entities.Cards.Card();
            payment.IsValid();

            var expected = new HttpResponseDTO<PaymentDTO>();
            expected.Status = 400;

            this.mockPaymentService
                .Setup(s => s.ProcessPaymentAsync(It.IsAny<Payment>()))
                .ReturnsAsync(payment);

            // Act
            var response = await paymentAppService.ProcessPaymentAsync(paymentDto);

            // Assert
            Assert.Equal(expected.Status, response.Status);
            Assert.NotNull(response.ErrorMessages);
        }
    }
}