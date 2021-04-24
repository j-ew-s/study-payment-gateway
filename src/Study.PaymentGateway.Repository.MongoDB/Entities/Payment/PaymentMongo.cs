using System;
using Study.PaymentGateway.Repository.MongoDB.Entities.Banks;
using Study.PaymentGateway.Repository.MongoDB.Entities.Bases;
using Study.PaymentGateway.Repository.MongoDB.Entities.Card;
using Study.PaymentGateway.Repository.MongoDB.Entities.Clients;

namespace Study.PaymentGateway.Repository.MongoDB.Entities.Payment
{
    public class PaymentMongo : IdentityMongo
    {
        public Guid MerchantId { get; set; }
        public BankResponseMongo BankResponse { get; set; }
        public ShopperMongo Shopper { get; set; }
        public CardMongo Card { get; set; }
        public decimal TotalCost { get; set; }
        public int Currency { get; set; }
    }
}