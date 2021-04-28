using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Study.PaymentGateway.Domain.Entities.Merchants;
using Study.PaymentGateway.Domain.Entities.Payments;
using Study.PaymentGateway.Domain.Services.Interfaces;

namespace Study.PaymentGateway.Domain.Services
{
    public class MerchantService : IMerchantService
    {
        public Task<Merchant> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Payment> Payment(Guid merchantId, Guid paymentId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Payment>> Payments(Guid merchantId)
        {
            throw new NotImplementedException();
        }
    }
}