namespace Study.PaymentGateway.Shared.Enums
{
    public enum BankResponseEnum
    {
        Success = 00,
        InvalidMerchant = 03,
        NotHonor = 05,
        ApprovedPartial = 10,
        InvalidInfo = 14,
        InsufficientFunds = 51,
        CardExpired = 54,
        NotFound = 100000
    }
}