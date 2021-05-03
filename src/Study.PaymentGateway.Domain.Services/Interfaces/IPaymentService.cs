using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Study.PaymentGateway.Domain.Entities.Paging;
using Study.PaymentGateway.Domain.Entities.Payments;

namespace Study.PaymentGateway.Domain.Services.Interfaces
{
    public interface IPaymentService
    {
        /// <summary>
        ///  Evaluate Payment and indicate if the Purchase is accept by Financial Institution.
        /// </summary>
        /// <param name="payment">Payment</param>
        /// <returns>True : Valid, False : Invalid</returns>
        Task<Payment> ProcessPaymentAsync(Payment payment);

        /// <summary>
        /// Get Payment by its ID
        /// </summary>
        /// <param name="id">(Guid) PaymentID that will be queried. </param>
        /// <returns>Return Payment object when its found.</returns>
        Task<Payment> GetByIdAsync(Guid id);

        /// <summary>
        /// Get Payments querying by Merchant Id
        /// </summary>
        /// <param name="id">(GUID) Merchant ID</param>
        /// <returns>Return list of Payment related to a MerchantID</returns>
        Task<IReadOnlyList<Payment>> GetPaymentByMerchantIdAsync(Guid id);

        /// <summary>
        /// Get Payments by CardNumber
        /// </summary>
        /// <param name="cardNumber">Int</param>
        /// <param name="currentPage">Current Page</param>
        /// <param name="itemsPerPage">Itens per page</param>
        /// <returns>a Paged object of Payment</returns>
        Task<PagedResult<Payment>> GetPaymentByCardNumberAsync(long cardNumber, int currentPage, int itemsPerPage);
    }
}