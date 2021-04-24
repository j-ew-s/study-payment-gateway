namespace Study.PaymentGateway.Domain.AcquiringBanksGateway
{
    using System;

    public interface IBankGateways
    {
        public string Token { get; set; }

        /// <summary>
        /// Logs to Banks API
        /// </summary>
        /// <param name="URL">Bank API address</param>
        /// <param name="user">PaymentGateway User</param>
        /// <param name="pass">PaymentGateway Password</param>
        void Login(string URL, string user, string pass);

        /// <summary>
        /// Executes the Payment.
        /// Acquiring Bank will validate and then process the payment
        /// </summary>
        /// <param name="URL">Bank API Address for payment</param>
        /// <param name="shopperCard">Shopper Card</param>
        /// <param name="merchant">Merchand Account</param>
        /// <returns>It will respond the Status of this Execution</returns>
        int ExecutesPayment(string URL, object shopperCard, object merchant);

        /// <summary>
        /// Sets the Bank configuration
        /// </summary>
        void GetBankConfiguration();
    }
}