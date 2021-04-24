using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Study.PaymentGateway.Domain.Entities.Paging;
using Study.PaymentGateway.Domain.Entities.Payments;

namespace Study.PaymentGateway.Domain.Repository
{
    public interface IPaymentRepository : IBaseRepository<Payment>
    {
        Task<IReadOnlyList<Payment>> GetPaymentByMerchantIdAsync(Guid id);

        Task<PagedResult<Payment>> GetPaymentByClientNameAsync(string clientName, int currentPage, int itemsPerPage);

        Task<PagedResult<Payment>> GetPaymentByCardNumberAsync(int cardNumber, int currentPage, int itemsPerPage);

        Task<PagedResult<Payment>> GetPaymentByTotalValueAsync(decimal totalValue, int currentPage, int itemsPerPage);
    }
}