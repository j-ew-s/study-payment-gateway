using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using Moq;
using Study.PaymentGateway.Domain.AcquiringBanksGateway.Services;
using Study.PaymentGateway.Domain.Entities.Banks;
using Study.PaymentGateway.Domain.Entities.Paging;
using Study.PaymentGateway.Domain.Entities.Payments;
using Study.PaymentGateway.Domain.Repository;
using Study.PaymentGateway.Domain.Services.Tests.Util.TestDataGenerator;
using Xunit;

namespace Study.PaymentGateway.Domain.Services.Tests
{
    public class PaymentServiceTest : BaseTest
    {
        public Fixture fixture = new Fixture();
        private PaymentService paymentService;
        private Mock<IPaymentRepository> mockPaymentRepository;
        private Mock<IGatewayServices> mockGatewayServices;

        public PaymentServiceTest()
        {
            this.mockPaymentRepository = new Mock<IPaymentRepository>();
            this.mockGatewayServices = new Mock<IGatewayServices>();

            this.paymentService = new PaymentService(this.mockPaymentRepository.Object, this.mockGatewayServices.Object);
        }

        [Fact]
        public async Task ProcessPayment_WhenValidPayment_ReturnsTrue()
        {
            // Arrange
            var payment = PaymentGenerate.GetValidPayment();
            this.mockPaymentRepository
                .Setup(s => s.InsertAsync(It.IsAny<Payment>()))
                .Verifiable();

            var bankResponse = new BankResponse()
            {
                Code = 00,
                Message = "Success",
                PaymentID = Guid.NewGuid().ToString()
            };

            this.mockGatewayServices
                .Setup(s => s.ExecutesPayment(It.IsAny<Payment>()))
                .ReturnsAsync(bankResponse);

            // Act
            var response = await paymentService.ProcessPaymentAsync(payment);

            // Assert
            Assert.True(response.IsValid());
            Assert.NotNull(response);

            this.mockPaymentRepository
                .Verify(
                    v => v.InsertAsync(It.IsAny<Payment>()),
                    Times.Once,
                    "PaymentRepository was not called");
        }

        [Fact]
        public async Task ProcessPayment_WhenNullPayment_ReturnsFalse()
        {
            // Arrange

            // Act
            var response = await paymentService.ProcessPaymentAsync(null);

            // Assert
            Assert.Null(response);

            this.mockPaymentRepository
                .Verify(
                    v => v.InsertAsync(It.IsAny<Payment>()),
                    Times.Never,
                    "PaymentRepository was called");
        }

        [Fact]
        public async Task ProcessPayment_WhenPaymentWithInvalidProperty_ReturnsFalse()
        {
            // Arrange
            var payment = PaymentGenerate.GetInvalidPayment_TotalCostZero();

            // Act
            var response = await paymentService.ProcessPaymentAsync(payment);

            // Assert
            Assert.False(response.IsValid());
            this.mockPaymentRepository
                .Verify(
                    v => v.InsertAsync(It.IsAny<Payment>()),
                    Times.Never,
                    "PaymentRepository was called");
        }

        [Fact]
        public async Task GetByIdAsync_When_GuidIsInvalid_Returns_Null()
        {
            // Arrange
            var paymentId = Guid.Empty;

            // Act
            var response = await paymentService.GetByIdAsync(paymentId);

            // Assert
            Assert.Null(response);
            this.mockPaymentRepository
                .Verify(
                    v => v.GetByIdAsync(It.Is<Guid>(s => s == paymentId)),
                    Times.Never,
                    "PaymentRepository should not being called");
        }

        [Fact]
        public async Task GetByIdAsync_When_GuidIsValid_Returns_Payment()
        {
            // Arrange
            var paymentId = Guid.NewGuid();
            var payments = this.fixture.Create<Payment>();

            this.mockPaymentRepository
                .Setup(s => s.GetByIdAsync(paymentId))
                .ReturnsAsync(payments);

            // Act
            var response = await paymentService.GetByIdAsync(paymentId);

            // Assert
            Assert.NotNull(response);

            this.mockPaymentRepository
                .Verify(
                    v => v.GetByIdAsync(It.IsAny<Guid>()),
                    Times.Once,
                    "PaymentRepository was called");
        }

        [Fact]
        public async Task GetPaymentByMerchantIdAsync_When_GuidIsInvalid_Returns_Null()
        {
            // Arrange
            var merchantId = Guid.Empty;

            // Act
            var response = await paymentService.GetPaymentByMerchantIdAsync(merchantId);

            // Assert
            Assert.Null(response);
            this.mockPaymentRepository
                .Verify(
                    v => v.GetPaymentByMerchantIdAsync(It.Is<Guid>(s => s == merchantId)),
                    Times.Never,
                    "PaymentRepository was called");
        }

        [Fact]
        public async Task GetPaymentByMerchantIdAsync_When_GuidIsValid_Returns_PaymentsList()
        {
            // Arrange
            var paymentId = Guid.NewGuid();
            var payments = this.fixture.Create<List<Payment>>();

            this.mockPaymentRepository
                .Setup(s => s.GetPaymentByMerchantIdAsync(paymentId))
                .ReturnsAsync(payments);

            // Act
            var response = await paymentService.GetPaymentByMerchantIdAsync(paymentId);

            // Assert
            Assert.NotNull(response);

            this.mockPaymentRepository
                .Verify(
                    v => v.GetPaymentByMerchantIdAsync(It.IsAny<Guid>()),
                    Times.Once,
                    "PaymentRepository was not called");
        }

        [Fact]
        public async Task GetPaymentByCardNumberAsync_When_GuidIsInvalid_Returns_Null()
        {
            // Arrange
            var cardNumber = 0;
            var currentPage = 0;
            var itemsPerPage = 10;

            // Act
            var response = await paymentService.GetPaymentByCardNumberAsync(cardNumber, currentPage, itemsPerPage);

            // Assert
            Assert.Null(response);
            this.mockPaymentRepository
                .Verify(
                    v => v.GetPaymentByCardNumberAsync(
                        It.Is<int>(s => s == cardNumber),
                        It.Is<int>(s => s == currentPage),
                        It.Is<int>(s => s == itemsPerPage)),
                    Times.Never,
                    "PaymentRepository was called");
        }

        [Fact]
        public async Task GetPaymentByCardNumberAsync_When_GuidIsValid_Returns_PaymentsList()
        {
            // Arrange
            var cardNumber = 1001001000010112;
            var currentPage = 0;
            var itemsPerPage = 10;

            var payments = this.fixture.Create<PagedResults<Payment>>();

            this.mockPaymentRepository
               .Setup(s => s.GetPaymentByCardNumberAsync(cardNumber, currentPage, itemsPerPage))
               .ReturnsAsync(payments);

            // Act
            var response = await paymentService.GetPaymentByCardNumberAsync(cardNumber, currentPage, itemsPerPage);

            // Assert
            Assert.NotNull(response);
            this.mockPaymentRepository
                .Verify(
                    v => v.GetPaymentByCardNumberAsync(
                        It.Is<int>(s => s == cardNumber),
                        It.Is<int>(s => s == currentPage),
                        It.Is<int>(s => s == itemsPerPage)),
                    Times.Never,
                    "PaymentRepository was not called");
        }
    }
}