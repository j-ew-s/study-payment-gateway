using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using Moq;
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

        public PaymentServiceTest()
        {
            this.mockPaymentRepository = new Mock<IPaymentRepository>();

            this.paymentService = new PaymentService(this.mockPaymentRepository.Object);
        }

        [Fact]
        public async Task ProcessPayment_WhenValidPayment_ReturnsTrue()
        {
            // Arrange
            var payment = PaymentGenerate.GetValidPayment();
            this.mockPaymentRepository
                .Setup(s => s.InsertAsync(It.IsAny<Payment>()))
                .Verifiable();

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
        public async Task GetByIdAsync_When_GuidIsValid_Returns_PaymentsList()
        {
            // Arrange
            var paymentId = Guid.NewGuid();
            List<Payment> payments = this.fixture.Create<List<Payment>>();

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
    }
}