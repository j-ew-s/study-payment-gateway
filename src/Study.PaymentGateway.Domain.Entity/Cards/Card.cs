using System;
using System.Collections.Generic;
using System.Linq;
using Study.PaymentGateway.Domain.Entities.Bases;
using Study.PaymentGateway.Shared.Enums;

namespace Study.PaymentGateway.Domain.Entities.Cards
{
    public class Card : Validation
    {
        public string Name { get; set; }
        public long Number { get; set; }
        public string CVV { get; set; }
        public string Expiration { get; set; }

        public BankCodeEnum Bank()
        {
            var bankNumber = int.Parse(this.Number.ToString().ToCharArray().FirstOrDefault().ToString());
            return (BankCodeEnum)bankNumber;
        }

        public override IReadOnlyList<string> GetErrorMessages()
        {
            var message = new List<string>();

            if (string.IsNullOrWhiteSpace(Name))
                message.Add("Name on Card is invalid");

            if (!IsNumberValid())
                message.Add("Card Number is invalid");

            if (!IsCVVValid())
                message.Add("Card CVV is invalid");

            if (!IsExpirationValid())
                message.Add("Card Expiration Date is invlaid");

            return message;
        }

        private bool IsNumberValid()
        {
            return Number != default && Number.ToString().Length == 16;
        }

        private bool IsCVVValid()
        {
            return CVV != default && CVV.Length == 3;
        }

        private bool IsExpirationValid()
        {
            if (!string.IsNullOrWhiteSpace(Expiration))
            {
                var month = Convert.ToInt16(Expiration.Substring(0, 2));
                var year = Convert.ToInt16(Expiration.Substring(3, 2));

                return ValidateDates(month, year);
            }

            return false;
        }

        private bool ValidateDates(int month, int year)
        {
            var currentYear = Convert.ToInt16(DateTime.Now.ToString("yy"));
            var currentMonth = Convert.ToInt16(DateTime.Now.Month);

            if (year < currentYear)
                return false;

            if (year == currentYear)
            {
                return month >= currentMonth;
            }

            return month < 13;
        }
    }
}