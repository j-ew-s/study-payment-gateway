using System;
using System.Collections.Generic;
using System.Linq;
using Study.PaymentGateway.Domain.Entities.Banks;
using Study.PaymentGateway.Domain.Entities.Bases;
using Study.PaymentGateway.Domain.Entities.Cards;
using Study.PaymentGateway.Domain.Entities.Clients;
using Study.PaymentGateway.Shared.Enums;

namespace Study.PaymentGateway.Domain.Entities.Payments
{
    public class Payment : Entity
    {
        public Payment()
        {
            BankResponse = null as BankResponse;
        }

        public Guid MerchantId { get; set; }
        public BankResponse BankResponse { get; set; }
        public Card Card { get; set; }
        public Shopper Shopper { get; set; }
        public decimal TotalCost { get; set; }
        public CurrencyEnum Currency { get; set; }
        public PaymentStatusEnum Status { get; set; }

        public override IReadOnlyList<string> GetErrorMessages()
        {
            var message = new HashSet<string>();

            if (MerchantId == Guid.Empty)
                message.Add("Merchant is required.");

            if (this.BankResponse != null)
                message.UnionWith(this.BankResponse.ErrorMessages);

            if (this.Card == null)
                message.Add("Card is required");
            else
                message.UnionWith(this.Card.ErrorMessages);

            if (this.Shopper == null)
                message.Add("Shopper is required");
            else
                message.UnionWith(this.Shopper.ErrorMessages);

            if (TotalCost < 0.01m)
                message.Add($"Total Cost should be greater than {TotalCost}");

            return message.ToList();
        }
    }
}