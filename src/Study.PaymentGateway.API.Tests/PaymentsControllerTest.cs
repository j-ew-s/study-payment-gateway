using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Study.PaymentGateway.API.Controllers.v1.Merchant;
using Study.PaymentGateway.API.Controllers.v1.Payments;
using Study.PaymentGateway.App.Services.Interfaces;
using Study.PaymentGateway.Shared.DTO.HTTPResponses;
using Study.PaymentGateway.Shared.DTO.Payments;
using Xunit;

namespace Study.PaymentGateway.API.Tests
{
    public class PaymentsControllerTest
    {
        private Mock<IPaymentAppService> paymentAppService;
        private PaymentsController paymentsController;
        private Fixture fixture;

        public PaymentsControllerTest()
        {
            this.paymentAppService = new Mock<IPaymentAppService>();
            this.paymentsController = new PaymentsController(this.paymentAppService.Object);
            this.fixture = new Fixture();
        }

        [Fact]
        public async Task PaymentsController_ProcessPayment_WhenTotalCostIsZeroOrLess_ShouldReturnBadRequest()
        {
            //Arrange
            var paymentDto = this.fixture.Create<PaymentDTO>();

            paymentDto.TotalCost = 0.00m;

            this.paymentAppService
                .Setup(s => s.ProcessPaymentAsync(It.IsAny<PaymentDTO>()))
                .ReturnsAsync(new HttpResponseDTO<PaymentDTO>
                {
                    ErrorMessages = It.IsAny<List<string>>(),
                    Response = paymentDto,
                    Status = (int)HttpStatusCode.BadRequest
                });

            // Act
            var response = await this.paymentsController.ProcessPayment(paymentDto);

            // Assert
            Assert.NotNull(response);
            var badrequestResult = response as BadRequestObjectResult;
            Assert.NotNull(badrequestResult);
        }

        [Fact]
        public async Task PaymentsController_ProcessPayment_WhenValidInput_ShouldReturnOk()
        {
            //Arrange
            var paymentDto = this.fixture.Create<PaymentDTO>();

            paymentDto.TotalCost = 100;

            this.paymentAppService
                .Setup(s => s.ProcessPaymentAsync(It.IsAny<PaymentDTO>()))
                .ReturnsAsync(new HttpResponseDTO<PaymentDTO>
                {
                    ErrorMessages = It.IsAny<List<string>>(),
                    Response = paymentDto,
                    Status = (int)HttpStatusCode.Created
                });

            // Act
            var response = await this.paymentsController.ProcessPayment(paymentDto);

            // Assert
            Assert.NotNull(response);
            var createdResult = response as CreatedAtActionResult;
            Assert.NotNull(createdResult);
            Assert.NotNull(createdResult.Value);
            Assert.Equal((int)HttpStatusCode.Created, createdResult.StatusCode);
        }
    }
}