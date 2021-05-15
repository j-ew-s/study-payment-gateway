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

        Task<PagedResults<Payment>> GetPaymentByCardNumberAsync(long cardNumber, int currentPage, int itemsPerPage);
    }
}