using System.Collections.Generic;
using Study.PaymentGateway.Domain.Entities.Addresses;
using Study.PaymentGateway.Domain.Entities.Bases;

namespace Study.PaymentGateway.Domain.Entities.Clients
{
    public class Shopper : Identity
    {
        public Shopper()
        {
            Address = new Address();
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }

        public override IReadOnlyList<string> GetErrorMessages()
        {
            var message = new List<string>();
            if (string.IsNullOrWhiteSpace(this.Name))
                message.Add("Shopper Name is required");

            if (string.IsNullOrWhiteSpace(this.Email))
                message.Add("Email is required");

            if (this.Address == null)
                message.Add("Address is required");
            else
                message.AddRange(this.Address.ErrorMessages);

            return message;
        }
    }
}