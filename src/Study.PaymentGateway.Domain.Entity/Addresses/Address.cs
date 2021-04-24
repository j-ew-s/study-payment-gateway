using System.Collections.Generic;
using Study.PaymentGateway.Domain.Entities.Bases;

namespace Study.PaymentGateway.Domain.Entities.Addresses
{
    public class Address : Validation
    {
        public string Street { get; set; }
        public string Complement { get; set; }
        public string ZIP { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }

        public override IReadOnlyList<string> GetErrorMessages()
        {
            var message = new List<string>();

            if (string.IsNullOrWhiteSpace(Street))
                message.Add("Street is required");

            if (string.IsNullOrWhiteSpace(ZIP))
                message.Add("ZIP is required");

            if (string.IsNullOrWhiteSpace(City))
                message.Add("City is required");

            if (string.IsNullOrWhiteSpace(State))
                message.Add("State is required");

            if (string.IsNullOrWhiteSpace(Country))
                message.Add("Country is required");

            return message;
        }
    }
}