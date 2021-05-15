using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using Moq;
using Study.PaymentGateway.App.Services.Tests.Util.DataGenerate;
using Study.PaymentGateway.Domain.Entities.Paging;
using Study.PaymentGateway.Domain.Entities.Payments;
using Study.PaymentGateway.Domain.Services.Interfaces;
using Study.PaymentGateway.Shared.DTO.HTTPResponses;
using Study.PaymentGateway.Shared.DTO.Payments;
using Study.PaymentGateway.Shared.DTO.QueryResponses.PagedItems;
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

            this.paymentAppService = new PaymentAppService(this.mockPaymentService.Object, this.mockMapper.Object);
        }

        [Fact]
        public async Task ProcessPayment_When_ValidPayment_Returns_HttpResponseStatus200()
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
        public async Task ProcessPaymentAsync_When_InvalidPayment_Returns_HttpResponseStatus400()
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

        [Fact]
        public async Task GetByIdAsync_When_ValidPayment_Returns_HttpResponseStatus200()
        {
            // Arrange
            var paymentresponseDTO = PaymentGenerate.GetValidPaymentResponseDTO();
            var payment = PaymentGenerate.GetValidPayment();

            var expected = new HttpResponseDTO<PaymentResponseDTO>();
            expected.Status = 200;
            expected.ErrorMessages = new List<string>();
            expected.Response = paymentresponseDTO;

            this.mockPaymentService
                .Setup(s => s.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(payment);

            this.mockMapper
                .Setup(x => x.Map<PaymentResponseDTO>(It.IsAny<Payment>()))
                .Returns(paymentresponseDTO);

            // Act
            var response = await paymentAppService.GetByIdAsync(paymentresponseDTO.Id);

            // Assert
            Assert.Equal(expected.Status, response.Status);
            Assert.Equal(expected.ErrorMessages, response.ErrorMessages);
        }

        [Fact]
        public async Task GetByIdAsync_When_ValidPayment_Returns_HttpResponseStatus44()
        {
            // Arrange
            var paymentDTO = new PaymentDTO();

            var payment = new Payment();

            var expected = new HttpResponseDTO<PaymentDTO>();
            expected.Status = 404;
            expected.ErrorMessages = new List<string>() { "Not Found" };
            expected.Response = paymentDTO;

            this.mockPaymentService
                .Setup(s => s.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(payment);

            this.mockMapper
                .Setup(x => x.Map<PaymentDTO>(It.IsAny<Payment>()))
                .Returns(paymentDTO);

            // Act
            var response = await paymentAppService.GetByIdAsync(paymentDTO.Id);

            // Assert
            Assert.Equal(expected.Status, response.Status);
            Assert.Equal(expected.ErrorMessages, response.ErrorMessages);
        }

        [Fact]
        public async Task GetPaymentByCardNumberAsync_When_ValidPayment_Returns_HttpResponseStatus200()
        {
            // Arrange
            var pagedResult = this.fixture.Create<PagedResults<Payment>>();

            var paymentResponseDTO = this.fixture.Create<PaymentResponseDTO>();
            paymentResponseDTO.Card.Number = 1234;

            var pagedResultDTO = this.fixture.Create<PagedResultsDTO<PaymentResponseDTO>>();
            pagedResultDTO.Records = new List<PaymentResponseDTO>() { paymentResponseDTO };

            var expected = new HttpResponseDTO<PagedResultsDTO<PaymentResponseDTO>>();
            expected.Status = 200;
            expected.ErrorMessages = new List<string>();
            expected.Response = pagedResultDTO;

            this.mockPaymentService
                .Setup(s => s.GetPaymentByCardNumberAsync(It.IsAny<long>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(pagedResult);

            this.mockMapper
                .Setup(x => x.Map<PagedResultsDTO<PaymentResponseDTO>>(pagedResult))
                .Returns(pagedResultDTO);

            // Act
            var response = await paymentAppService.GetPaymentByCardNumberAsync(paymentResponseDTO.Card.Number, 0, 0);

            // Assert
            Assert.Equal(expected.Status, response.Status);
            Assert.Equal(expected.ErrorMessages, response.ErrorMessages);
        }

        [Fact]
        public async Task GetPaymentByCardNumberAsync_When_InvalidValidPayment_Return_HttpResponseStatus204()
        {
            // Arrange
            var paymentDTO = PaymentGenerate.GetValidPaymentDTO();

            var pagedMapped = new PagedResultsDTO<PaymentResponseDTO>();

            var expected = new HttpResponseDTO<PagedResultsDTO<PaymentResponseDTO>>();
            expected.Status = 204;
            expected.ErrorMessages = pagedMapped.Messages();
            expected.Response = null;

            this.mockPaymentService
                .Setup(s => s.GetPaymentByCardNumberAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(It.IsAny<PagedResults<Payment>>());

            this.mockMapper
                .Setup(x => x.Map<PagedResultsDTO<PaymentResponseDTO>>(It.IsAny<Payment>()))
                .Returns(pagedMapped);

            // Act
            var response = await paymentAppService.GetPaymentByCardNumberAsync(paymentDTO.Card.Number, 0, 0);

            // Assert
            Assert.Equal(expected.Status, response.Status);
            Assert.Equal(expected.ErrorMessages, response.ErrorMessages);
        }
    }
}