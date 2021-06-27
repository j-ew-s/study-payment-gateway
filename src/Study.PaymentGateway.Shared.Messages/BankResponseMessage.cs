using Study.PaymentGateway.Shared.Enums;

namespace Study.PaymentGateway.Shared.Messages
{
    public static class BankResponseMessage
    {
        private const string APPROVED = "Payment were concluded with Success.";
        private const string APPROVED_PARTIAL = "Payment were concluded with Success.";
        private const string INVALID_MERCHANT = "Invalid merchant.";
        private const string INVALID_INFO = "Card have some invalid info.";
        private const string INVALID_CARD_EXPIRED = "The credit card being used has expired.";
        private const string DO_NOT_HONOR = "The Issuing Bank's fraud filter has been triggered.";
        private const string INSUFFICIENT_FUNDS = "There are not enough funds in the associated bank accunt.";
        private const string RESPONSE_NOT_FOUND = "There is no valid response.";

        public static string GetMessage(BankResponseEnum actionCode)
        {
            switch (actionCode)
            {
                case BankResponseEnum.Success:
                    return APPROVED;

                case BankResponseEnum.InvalidMerchant:
                    return INVALID_MERCHANT;

                case BankResponseEnum.NotHonor:
                    return DO_NOT_HONOR;

                case BankResponseEnum.ApprovedPartial:
                    return APPROVED_PARTIAL;

                case BankResponseEnum.InvalidInfo:
                    return INVALID_INFO;

                case BankResponseEnum.InsufficientFunds:
                    return INSUFFICIENT_FUNDS;

                case BankResponseEnum.CardExpired:
                    return INVALID_CARD_EXPIRED;

                default:
                    return RESPONSE_NOT_FOUND;
            }
        }
    }
}