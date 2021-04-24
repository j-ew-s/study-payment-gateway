using System.Collections.Generic;
using Study.PaymentGateway.Domain.Entities.Addresses;
using Study.PaymentGateway.Domain.Entities.Bases;

namespace Study.PaymentGateway.Domain.Entities.Merchants
{
    public class Merchant : Entity
    {
        public Merchant()
        {
            Address = new Address();
        }

        public string Name { get; set; }
        public Address Address { get; set; }

        public override IReadOnlyList<string> GetErrorMessages()
        {
            var messages = new List<string>();

            if (string.IsNullOrWhiteSpace(Name))
                messages.Add("Name is required.");

            if (Address == null || Address.IsValid())
            {
                messages.Add("Address is required");
            }

            return messages;
        }
    }
}