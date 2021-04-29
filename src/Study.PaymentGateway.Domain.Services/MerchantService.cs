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
        public async Task<Merchant> Get(Guid id)
        {
            if (Guid.Empty.Equals(id))
                return null;

            return new Merchant();
        }

        public async Task<Payment> Payment(Guid merchantId, Guid paymentId)
        {
            if (Guid.Empty.Equals(merchantId) ||
                Guid.Empty.Equals(paymentId))
            {
                return null;
            }

            return new Payment();
        }

        public async Task<List<Payment>> Payments(Guid merchantId)
        {
            var payments = new List<Payment>();

            if (Guid.Empty.Equals(merchantId))
                return null;

            return payments;
        }
    }
}