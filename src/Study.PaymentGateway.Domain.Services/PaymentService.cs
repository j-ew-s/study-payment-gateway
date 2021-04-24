using System;
using System.Threading.Tasks;
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

        public async Task<Payment> ProcessPayment(Payment payment)
        {
            if (payment == null || !payment.IsValid())
                return payment;

            payment.CreatedAt = DateTime.Now;
            payment.UpdatedAt = DateTime.Now;

            await this.paymentRepository.InsertAsync(payment);

            return payment;
        }
    }
}