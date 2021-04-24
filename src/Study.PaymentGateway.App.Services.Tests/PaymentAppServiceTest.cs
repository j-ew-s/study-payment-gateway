using System;
using System.Threading.Tasks;
using AutoFixture;
using AutoMapper;
using Moq;
using Study.PaymentGateway.App.Services.Tests.Util.DataGenerate;
using Study.PaymentGateway.Domain.Entities.Banks;
using Study.PaymentGateway.Domain.Entities.Cards;
using Study.PaymentGateway.Domain.Entities.Payments;
using Study.PaymentGateway.Domain.Repository;
using Study.PaymentGateway.Domain.Services;
using Study.PaymentGateway.Domain.Services.Interfaces;
using Study.PaymentGateway.Repository.MongoDB.Repository;
using Study.PaymentGateway.Shared.DTO.Cards;
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
                .Setup(s => s.ProcessPayment(It.IsAny<Payment>()))
                .ReturnsAsync(payment);

            this.mockMapper
                .Setup(x => x.Map<Payment>(It.IsAny<PaymentDTO>()))
                .Returns(payment);

            this.mockMapper
                .Setup(x => x.Map<PaymentDTO>(It.IsAny<Payment>()))
                .Returns(paymentDTO);

            // Act
            var response = await paymentAppService.ProcessPayment(paymentDTO);

            // Assert
            Assert.Equal(expected.Status, response.Status);
            Assert.Equal(expected.ErrorMessages, response.ErrorMessages);
        }

        [Fact]
        public async Task ProcessPayment_WhenInvalidValidPayment_ReturnHttpResponseStatus400()
        {
            // Arrange
            var payment = fixture.Create<PaymentDTO>();
            payment.Card = new Shared.DTO.Cards.CardDTO();

            var expected = new HttpResponseDTO<PaymentDTO>();
            expected.Status = 400;

            // Act
            var response = await paymentAppService.ProcessPayment(payment);

            // Assert
            Assert.Equal(expected.Status, response.Status);
            Assert.NotNull(response.ErrorMessages);
        }
    }
}