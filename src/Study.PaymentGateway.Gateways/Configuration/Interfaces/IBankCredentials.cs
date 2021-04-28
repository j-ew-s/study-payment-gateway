namespace Study.PaymentGateway.Gateways.Configuration.Interfaces
{
    public interface IBankCredentials
    {
        public string User { get; set; }
        public string Password { get; set; }
    }
}