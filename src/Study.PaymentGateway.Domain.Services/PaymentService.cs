using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Study.PaymentGateway.Domain.Entities.Paging;
using Study.PaymentGateway.Domain.Entities.Payments;
using Study.PaymentGateway.Domain.Repository;
using Study.PaymentGateway.Domain.Services.Interfaces;

namespace Study.PaymentGateway.Domain.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository paymentRepository;

        public PaymentService(IPaymentRepository paymentRepository)
        {
            this.paymentRepository = paymentRepository;
        }

        public async Task<Payment> GetByIdAsync(Guid id)
        {
            if (Guid.Empty.Equals(id))
                return null;

            return await this.paymentRepository.GetByIdAsync(id);
        }

        public async Task<PagedResults<Payment>> GetPaymentByCardNumberAsync(long cardNumber, int currentPage, int itemsPerPage)
        {
            if (cardNumber == default)
                return null;

            if (itemsPerPage == 0)
                itemsPerPage = 10;

            return await this.paymentRepository.GetPaymentByCardNumberAsync(cardNumber, currentPage, itemsPerPage);
        }

        public async Task<IReadOnlyList<Payment>> GetPaymentByMerchantIdAsync(Guid id)
        {
            if (Guid.Empty.Equals(id))
                return null;

            var payment = await this.paymentRepository.GetPaymentByMerchantIdAsync(id);

            return payment.ToList();
        }

        public async Task<Payment> ProcessPaymentAsync(Payment payment)
        {
            if (payment == null || !payment.IsValid())
                return payment;

            payment.CreatedAt = DateTime.Now;
            payment.UpdatedAt = DateTime.Now;

            this.paymentRepository.InsertAsync(payment);

            return payment;
        }
    }
}