using System.Collections.Generic;
using Study.PaymentGateway.Domain.Entities.Bases;
using Study.PaymentGateway.Shared.Messages;

namespace Study.PaymentGateway.Domain.Entities.Banks
{
    public class BankResponse : Validation
    {
        public string PaymentID { get; set; }
        public int Code { get; set; }
        public string Message { get; set; }

        public override IReadOnlyList<string> GetErrorMessages()
        {
            var message = new List<string>();

            if (Code != 00)
                message.Add(BankResponseMessage.GetMessage(Code));

            return message;
        }
    }
}