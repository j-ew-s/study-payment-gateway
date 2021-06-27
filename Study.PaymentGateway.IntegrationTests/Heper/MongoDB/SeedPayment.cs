using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Bson;
using MongoDB.Driver;
using Study.PaymentGateway.API;
using Study.PaymentGateway.Domain.Entities.Cards;
using Study.PaymentGateway.Domain.Entities.Payments;
using Study.PaymentGateway.IntegrationTests.Base;
using Study.PaymentGateway.Repository.MongoDB.Configuration.Interfaces;
using Study.PaymentGateway.Repository.MongoDB.Entities.Payment;
using Study.PaymentGateway.Repository.MongoDB.Repository;

namespace Study.PaymentGateway.IntegrationTests.Heper.MongoDB
{
    public class SeedPayment : WebApplicationFactory<Startup>
    {
        private readonly IHost host;
        private readonly PaymentRepository paymentRepository;
        private Guid MerchantId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6");
        private IMongoDBConfiguration iMongoconfig;
        private IMapper iMapper;

        public SeedPayment(IHost host)
        {
            this.host = host;

            this.getInstance();

            this.paymentRepository = new PaymentRepository(this.iMongoconfig, this.iMapper);
        }

        public Payment InsertPayment(Guid paymentId)
        {
            var payment = CreatePayment();

            payment.CreatedAt = DateTime.Now;
            payment.UpdatedAt = DateTime.Now;
            payment.Id = paymentId;

            this.paymentRepository.InsertAsync(payment);

            return payment;
        }

        public void Cleanup(Payment payment)
        {
            var deleteFilter = Builders<PaymentMongo>.Filter.Eq("_id", payment.Id);

            this.iMongoconfig.Payment.DeleteOneAsync(deleteFilter);
        }

        private void getInstance()
        {
            if (this.host == null)
            {
                throw new ArgumentNullException("host");
            }

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                this.iMongoconfig = services.GetRequiredService<IMongoDBConfiguration>();
                this.iMapper = services.GetRequiredService<IMapper>();
            }
        }

        private Payment CreatePayment()
        {
            return new Payment()
            {
                BankResponse = new Domain.Entities.Banks.BankResponse()
                {
                    Code = 10,
                    Message = string.Empty,
                    PaymentID = "payment-1",
                },
                Card = new Card()
                {
                    CVV = "999",
                    Expiration = "25/99",
                    Name = "user name",
                    Number = 9999000088889999
                },
                Shopper = new Domain.Entities.Clients.Shopper()
                {
                    Name = "Fake name",
                    Address = new Domain.Entities.Addresses.Address(),
                    Email = "fake@email.com",
                },
                MerchantId = this.MerchantId,
                TotalCost = 10.50m,
                Currency = Shared.Enums.CurrencyEnum.USD
            };
        }
    }
}