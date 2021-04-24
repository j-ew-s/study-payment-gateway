using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Study.PaymentGateway.API.Controllers.Base;
using Study.PaymentGateway.Shared.DTO.HTTPResponses;
using Study.PaymentGateway.Shared.DTO.Payments;
using Xunit;

namespace Study.PaymentGateway.API.Tests
{
    public class BaseControllerTest
    {
        private Fixture fixture = new Fixture();
        private BaseController baseController = new BaseController();

        [Fact]
        public void BaseController_ProcessResponseWithRedirect_WhenInputIsInvalid_ShouldReturnInternalServerError()
        {
            //Arrange
            var httpResponseDto = null as HttpResponseDTO<PaymentDTO>;
            // Act
            var response = this.baseController.ProcessResponse(httpResponseDto);
            var statusCode = response as StatusCodeResult;
            // Assert
            Assert.NotNull(response);
            Assert.Equal(500, statusCode.StatusCode);
        }

        [Fact]
        public void BaseController_ProcessResponse_WhenInputIsInvalid_ShouldReturnInternalServerError()
        {
            //Arrange
            var httpResponseDto = null as HttpResponseDTO<PaymentDTO>;
            // Act
            var response = this.baseController.ProcessResponse(httpResponseDto);
            var statusCode = response as StatusCodeResult;
            // Assert
            Assert.NotNull(response);
            Assert.Equal(500, statusCode.StatusCode);
        }

        [Theory]
        [InlineData(200, 200)]
        [InlineData(204, 204)]
        [InlineData(400, 400)]
        [InlineData(404, 404)]
        public void BaseController_ProcessResponse_WhenInputIsValid_ShouldReturnSuccessStatus(int status, int expectedStatus)
        {
            //Arrange
            var httpResponseDto = this.fixture.Create<HttpResponseDTO<PaymentDTO>>();
            httpResponseDto.Status = status;

            // Act
            var response = this.baseController.ProcessResponse(httpResponseDto);

            // Assert
            int statusCode;
            if (status == 204)
            {
                var parsedResponse = response as StatusCodeResult;
                statusCode = parsedResponse.StatusCode;
            }
            else
            {
                var parsedResponse = response as ObjectResult;
                statusCode = parsedResponse.StatusCode.Value;
            };

            Assert.NotNull(response);
            Assert.Equal(expectedStatus, statusCode);
        }
    }
}