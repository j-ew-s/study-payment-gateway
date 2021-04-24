using System;

namespace Study.PaymentGateway.Domain.Entities.Bases
{
    public abstract class Entity : Identity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}