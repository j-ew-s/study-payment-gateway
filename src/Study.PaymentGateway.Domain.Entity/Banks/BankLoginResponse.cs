namespace Study.PaymentGateway.Domain.Entities.Banks
{
    public class BankLoginResponse
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public string Body { get; set; }
    }
}