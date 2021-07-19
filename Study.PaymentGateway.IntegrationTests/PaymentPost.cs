using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Study.PaymentGateway.IntegrationTests.Base;
using Study.PaymentGateway.IntegrationTests.Heper.MongoDB;
using Study.PaymentGateway.Shared.DTO.HTTPResponses;
using Study.PaymentGateway.Shared.DTO.Payments;
using Xunit;

namespace Study.PaymentGateway.IntegrationTests
{
    public class PaymentPost : IClassFixture<BaseIntegrationTests>
    {
        private HttpClient httpClient;
        private SeedPayment seedPayment;

        public PaymentPost(BaseIntegrationTests baseTests)
        {
            this.httpClient = baseTests.CreateClient();

            this.seedPayment = new SeedPayment(baseTests.HostWeb);
        }

        [Fact]
        public async Task Paymentcontroller_ProcessPayment_WhenPaymentIsValid_ShouldReturnThePaymentAndStatusCode201()
        {
            // arrange
            var paymentId = Guid.Parse("403ea23e-cb04-41c6-9220-9a348c6ca444");
            var payment = this.seedPayment.InsertPayment(paymentId);

            var httpContent = new StringContent(JsonConvert.SerializeObject(payment), Encoding.UTF8, "application/json");

            var request = $"api/payments";

            // act
            var response = await this.httpClient.PostAsync(request, httpContent);

            // Assert
            Assert.NotNull(response);

            var httpResponseDto = await ExtractHttpResponseMessageContent(response);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.False(httpResponseDto.ErrorMessages.Any());
            Assert.NotNull(httpResponseDto.Response);

            this.seedPayment.Cleanup(payment);
        }

        [Fact]
        public async Task Paymentcontroller_ProcessPayment_WhenPaymentIsInalid_ShouldReturnThePaymentAndStatusCod401()
        {
            // arrange
            var paymentId = Guid.Parse("403ea23e-cb04-41c6-9220-9a348c6ca444");
            var payment = this.seedPayment.CreatePayment();
            payment.Id = paymentId;
            payment.CreatedAt = DateTime.UtcNow;
            payment.UpdatedAt = DateTime.UtcNow;
            payment.Card = new Domain.Entities.Cards.Card()
            {
                CVV = "0",
                Expiration = "12/00",
                Name = " ",
                Number = 4567867899
            };

            payment = this.seedPayment.InsertPayment(payment);

            var httpContent = new StringContent(JsonConvert.SerializeObject(payment), Encoding.UTF8, "application/json");

            var request = $"api/payments";

            // act
            var response = await this.httpClient.PostAsync(request, httpContent);

            // Assert
            Assert.NotNull(response);
            var httpResponseDto = await ExtractHttpResponseMessageContent(response);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.True(httpResponseDto.ErrorMessages.Any());
            Assert.NotNull(httpResponseDto.ErrorMessages.Where(w => w.Equals("Name on Card is invalid")));

            this.seedPayment.Cleanup(payment);
        }

        private async Task<HttpResponseDTO<PaymentResponseDTO>> ExtractHttpResponseMessageContent(HttpResponseMessage response)
        {
            var requestResult = await response.Content.ReadAsStringAsync();
            JObject json = JObject.Parse(requestResult);
            return JsonConvert.DeserializeObject<HttpResponseDTO<PaymentResponseDTO>>(requestResult);
        }
    }
}