using System;
using System.ComponentModel.DataAnnotations;

namespace Study.PaymentGateway.Shared.DTO.Cards
{
    public class CardDTO
    {
        public string Name { get; set; }
        public long Number { get; set; }

        [MaxLength(3, ErrorMessage = "CVV should have 3 digits")]
        public string CVV { get; set; }

        public string Expiration { get; set; }
    }
}