using System;
using System.ComponentModel.DataAnnotations;
using Study.PaymentGateway.Shared.DTO.Bases;
using Study.PaymentGateway.Shared.DTO.Cards;
using Study.PaymentGateway.Shared.DTO.Clients;
using Study.PaymentGateway.Shared.Enums;

namespace Study.PaymentGateway.Shared.DTO.Payments
{
    public class PaymentDTO : IdentityDTO
    {
        [Required]
        public Guid MerchantId { get; set; }

        [Required]
        public ShopperDTO Shopper { get; set; }

        [Required]
        public CardDTO Card { get; set; }

        [Required]
        public decimal TotalCost { get; set; }

        [Required]
        public CurrencyEnum Currency { get; set; }
    }
}