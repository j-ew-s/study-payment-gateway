namespace Study.PaymentGateway.Repository.MongoDB.Entities.Banks
{
    public class BankResponseMongo
    {
        public string PaymentID { get; set; }
        public int code { get; set; }
        public string Message { get; set; }
    }
}