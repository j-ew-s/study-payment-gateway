using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Study.PaymentGateway.IntegrationTests.Base;
using Study.PaymentGateway.IntegrationTests.Heper.MongoDB;
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
        public async Task Paymentcontroller_GetByid_WhenPaymentIdExists_ShouldReturnThePaymentAndStatusCode200()
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