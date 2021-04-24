using System;

namespace Study.PaymentGateway.Domain.Entities.Bases
{
    public abstract class Identity : Validation
    {
        private Guid id;

        public Guid Id
        {
            get { return id; }
            set { id = SetId(value); }
        }

        private Guid SetId(Guid value)
        {
            if (Guid.Empty.Equals(value))
            {
                return Guid.NewGuid();
            }

            return value;
        }
    }
}