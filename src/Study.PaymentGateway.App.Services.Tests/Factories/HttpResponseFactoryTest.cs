using System.Collections.Generic;
using AutoFixture;
using Study.PaymentGateway.App.Services.Factories;
using Study.PaymentGateway.App.Services.Factories.Enums;
using Study.PaymentGateway.Domain.Entities.Payments;
using Study.PaymentGateway.Shared.DTO.HTTPResponses;
using Study.PaymentGateway.Shared.DTO.Payments;
using Xunit;

namespace Study.PaymentGateway.App.Services.Tests.Factories
{
    public class HttpResponseFactoryTest
    {
        private Fixture fixture = new Fixture();

        [Theory]
        [InlineData(HttpActionEnum.Insert, 201)]
        [InlineData(HttpActionEnum.Delete, 200)]
        [InlineData(HttpActionEnum.Update, 200)]
        [InlineData(HttpActionEnum.Get, 200)]
        [InlineData(HttpActionEnum.GetQueryString, 200)]
        public void Create_WhenMessagesIsNull_ShouldReturnSuccessfulStatusCodes(HttpActionEnum action, int httpStatusExpected)
        {
            // Arrange
            var paymentDto = fixture.Create<PaymentDTO>();

            var messages = new List<string>();
            // Act
            var response = HttpResponseFactory.Create(paymentDto, messages, action);

            // Assert
            Assert.Equal(httpStatusExpected, response.Status);
        }

        [Theory]
        [InlineData(HttpActionEnum.Insert, 400)]
        [InlineData(HttpActionEnum.Delete, 400)]
        [InlineData(HttpActionEnum.Update, 400)]
        [InlineData(HttpActionEnum.Get, 404)]
        [InlineData(HttpActionEnum.GetQueryString, 204)]
        public void Create_WhenMessagesIsNotNull_ShouldReturnUnsuccessfulStatusCodes(HttpActionEnum action, int httpStatusExpected)
        {
            // Arrange
            var paymentDto = fixture.Create<PaymentDTO>();
            var payment = fixture.Create<Payment>();
            payment.Card = new Domain.Entities.Cards.Card();

            var messages = payment.ErrorMessages;
            // Act
            var response = HttpResponseFactory.Create(paymentDto, messages, action);

            // Assert
            Assert.Equal(httpStatusExpected, response.Status);
        }
    }
}