using System.Collections.Generic;
using Study.PaymentGateway.Domain.Entities.Bases;
using Study.PaymentGateway.Shared.Enums;
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

            if (Code != (int)BankResponseEnum.Success)
            {
                var code = (BankResponseEnum)Code;
                message.Add(BankResponseMessage.GetMessage(code));
            }

            return message;
        }
    }
}