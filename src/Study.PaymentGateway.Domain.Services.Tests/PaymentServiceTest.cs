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
            var response = await paymentService.ProcessPayment(payment);

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
            var response = await paymentService.ProcessPayment(null);

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
            var response = await paymentService.ProcessPayment(payment);

            // Assert
            Assert.False(response.IsValid());
            this.mockPaymentRepository
                .Verify(
                    v => v.InsertAsync(It.IsAny<Payment>()),
                    Times.Never,
                    "PaymentRepository was called");
        }
    }
}