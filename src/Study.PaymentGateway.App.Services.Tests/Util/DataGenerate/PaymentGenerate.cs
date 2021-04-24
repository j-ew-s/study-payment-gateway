using System;
using AutoFixture;
using Study.PaymentGateway.Domain.Entities.Banks;
using Study.PaymentGateway.Domain.Entities.Cards;
using Study.PaymentGateway.Domain.Entities.Payments;
using Study.PaymentGateway.Shared.DTO.Cards;
using Study.PaymentGateway.Shared.DTO.Payments;

namespace Study.PaymentGateway.App.Services.Tests.Util.DataGenerate
{
    internal static class PaymentGenerate
    {
        private static Fixture fixture = new Fixture();

        public static Payment GetValidPayment()
        {
            var year = DateTime.Now.ToString("yy");
            var month = DateTime.Now.Month.ToString("d2");

            var card = new Card()
            {
                CVV = "333",
                Number = 1236547893211234,
                Expiration = $"{month}/{year}",
                Name = "Test Name"
            };

            var bankResponse = new BankResponse()
            {
                Code = 00,
                Message = default,
                PaymentID = Guid.NewGuid().ToString()
            };

            var payment = fixture
               .Build<Payment>()
               .With(w => w.Card, card)
               .With(w => w.BankResponse, bankResponse)
               .Create();

            return payment;
        }

        public static PaymentDTO GetValidPaymentDTO()
        {
            var year = DateTime.Now.ToString("yy");
            var month = DateTime.Now.Month.ToString("d2");

            var cardDTO = new CardDTO()
            {
                CVV = "333",
                Number = 1236547893211234,
                Expiration = $"{month}/{year}",
                Name = "Test Name"
            };

            var paymentDTO = fixture
                .Build<PaymentDTO>()
                .With(w => w.Card, cardDTO)
                .Create();

            return paymentDTO;
        }
    }
}