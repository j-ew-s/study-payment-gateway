using System;
using AutoFixture;
using Study.PaymentGateway.Domain.Entities.Banks;
using Study.PaymentGateway.Domain.Entities.Cards;
using Study.PaymentGateway.Domain.Entities.Payments;

namespace Study.PaymentGateway.Gateways.Tests.TestHelpers.Data
{
    public static class PaymentBuilder
    {
        private static Fixture fixture = new Fixture();

        public static Payment GetValidPayment()
        {
            var year = DateTime.Now.ToString("yy");
            var month = DateTime.Now.Month.ToString("d2");

            var card = new Card()
            {
                CVV = "333",
                Number = 4236547893211234,
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

        public static Payment GetInvalidPayment_TotalCostZero()
        {
            var payment = GetValidPayment();

            payment.TotalCost = 0;

            return payment;
        }
    }
}