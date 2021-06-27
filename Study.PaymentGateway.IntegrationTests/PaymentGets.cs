using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Study.PaymentGateway.IntegrationTests.Base;
using Study.PaymentGateway.IntegrationTests.Heper.MongoDB;
using Study.PaymentGateway.Shared.DTO.HTTPResponses;
using Study.PaymentGateway.Shared.DTO.Payments;
using Study.PaymentGateway.Shared.DTO.QueryResponses.PagedItems;
using Xunit;

namespace Study.PaymentGateway.IntegrationTests
{
    public class PaymentGets : IClassFixture<BaseIntegrationTests>
    {
        private HttpClient httpClient;
        private SeedPayment seedPayment;

        public PaymentGets(BaseIntegrationTests baseTests)
        {
            this.httpClient = baseTests.CreateClient();

            this.seedPayment = new SeedPayment(baseTests.HostWeb);
        }

        [Fact]
        public async Task GetByid()
        {
            // arrange
            var paymentId = Guid.Parse("403ea23e-cb04-41c6-9220-9a348c6ca444");
            var payment = this.seedPayment.InsertPayment(paymentId);

            var request = $"api/payments/{payment.Id}";

            // act
            var response = await this.httpClient.GetAsync(request);

            // Assert
            Assert.NotNull(response);
            var requestResult = await response.Content.ReadAsStringAsync();
            Assert.NotNull(requestResult);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            this.seedPayment.Cleanup(payment);
        }
    }
}