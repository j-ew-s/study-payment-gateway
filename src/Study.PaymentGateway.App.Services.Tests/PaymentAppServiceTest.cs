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

            this.paymentAppService = new PaymentAppService(this.mockPaymentService.Object, this.Map);
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
            var paymentDTO = PaymentGenerate.GetValidPaymentDTO();

            var payment = PaymentGenerate.GetValidPayment();

            var expected = new HttpResponseDTO<PaymentDTO>();
            expected.Status = 200;
            expected.ErrorMessages = new System.Collections.Generic.List<string>();
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
            var paymentDTO = PaymentGenerate.GetValidPaymentDTO();

            var payment = PaymentGenerate.GetValidPayment();
            var pagedResult = this.fixture.Create<PagedResult<Payment>>();

            var pagedMapped = this.fixture.Create<PagedResultDTO<PaymentDTO>>();

            var fixturePaymentDTO = this.fixture.Create<PaymentDTO>();
            fixturePaymentDTO.Card.CVV = "333";
            fixturePaymentDTO.Card.Number = 1231231231231234;
            fixturePaymentDTO.Card.Expiration = "12/21";

            pagedMapped.Records = new List<PaymentDTO>() { fixturePaymentDTO };

            var expected = new HttpResponseDTO<PaymentDTO>();
            expected.Status = 200;
            expected.ErrorMessages = new List<string>();
            expected.Response = paymentDTO;

            this.mockPaymentService
                .Setup(s => s.GetPaymentByCardNumberAsync(It.IsAny<long>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(pagedResult);

            this.mockMapper
                .Setup(x => x.Map<PagedResultDTO<PaymentDTO>>(pagedResult))
                .Returns(pagedMapped);

            // Act
            var response = await paymentAppService.GetPaymentByCardNumberAsync(paymentDTO.Card.Number, 0, 0);

            // Assert
            Assert.Equal(expected.Status, response.Status);
            Assert.Equal(expected.ErrorMessages, response.ErrorMessages);
        }

        [Fact]
        public async Task GetPaymentByCardNumberAsync_When_InvalidValidPayment_Return_HttpResponseStatus204()
        {
            // Arrange
            var paymentDTO = PaymentGenerate.GetValidPaymentDTO();

            var payment = PaymentGenerate.GetValidPayment();
            PagedResult<Payment> pagedResult = new PagedResult<Payment>();
            PagedResultDTO<PaymentDTO> pagedMapped = new PagedResultDTO<PaymentDTO>();

            var expected = new HttpResponseDTO<PaymentDTO>();
            expected.Status = 204;
            expected.ErrorMessages = pagedMapped.Messages();
            expected.Response = null;

            this.mockPaymentService
                .Setup(s => s.GetPaymentByCardNumberAsync(It.IsAny<long>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(pagedResult);

            this.mockMapper
                .Setup(x => x.Map<PagedResultDTO<PaymentDTO>>(pagedResult))
                .Returns(pagedMapped);

            // Act
            var response = await paymentAppService.GetPaymentByCardNumberAsync(paymentDTO.Card.Number, 0, 0);

            // Assert
            Assert.Equal(expected.Status, response.Status);
            Assert.Equal(expected.ErrorMessages, response.ErrorMessages);
        }
    }
}