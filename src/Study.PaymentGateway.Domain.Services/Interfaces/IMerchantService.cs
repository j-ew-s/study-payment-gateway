using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Study.PaymentGateway.Domain.Entities.Merchants;
using Study.PaymentGateway.Domain.Entities.Payments;

namespace Study.PaymentGateway.Domain.Services.Interfaces
{
    public interface IMerchantService
    {
        Task<Merchant> Get(Guid id);

        Task<List<Payment>> Payments(Guid merchantId);

        Task<Payment> Payment(Guid merchantId, Guid paymentId);
    }
}