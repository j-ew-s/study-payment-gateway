using System;
using Study.PaymentGateway.Shared.DTO.Bases;
using Study.PaymentGateway.Shared.DTO.Cards;
using Study.PaymentGateway.Shared.DTO.Clients;
using Study.PaymentGateway.Shared.Enums;

namespace Study.PaymentGateway.Shared.DTO.Payments
{
    public class PaymentResponseDTO : IdentityDTO
    {
        public Guid MerchantId { get; set; }

        public ShopperDTO Shopper { get; set; }

        public CardResponseDTO Card { get; set; }

        public decimal TotalCost { get; set; }

        public CurrencyEnum Currency { get; set; }
    }
}