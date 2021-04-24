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

        public static string GetMessage(int actionCode)
        {
            switch (actionCode)
            {
                case 00:
                    return APPROVED;

                case 03:
                    return INVALID_MERCHANT;

                case 05:
                    return DO_NOT_HONOR;

                case 10:
                    return APPROVED_PARTIAL;

                case 14:
                    return INVALID_INFO;

                case 51:
                    return INSUFFICIENT_FUNDS;

                case 54:
                    return INVALID_CARD_EXPIRED;

                default:
                    return RESPONSE_NOT_FOUND;
            }
        }
    }
}