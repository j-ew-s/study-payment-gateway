using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Study.PaymentGateway.Domain.AcquiringBanksGateway.Factory;
using Study.PaymentGateway.Domain.AcquiringBanksGateway.Services;
using Study.PaymentGateway.Domain.Entities.Paging;
using Study.PaymentGateway.Domain.Entities.Payments;
using Study.PaymentGateway.Domain.Repository;
using Study.PaymentGateway.Domain.Services.Interfaces;
using Study.PaymentGateway.Shared.Enums;

namespace Study.PaymentGateway.Domain.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository paymentRepository;
        private readonly IGatewayServices gatewayServices;

        public PaymentService(IPaymentRepository paymentRepository, IGatewayServices gatewayServices)
        {
            this.paymentRepository = paymentRepository;
            this.gatewayServices = gatewayServices;
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
            if (!IsPaymentValid(payment))
                return payment;

            payment.BankResponse = await gatewayServices.ExecutesPayment(payment);

            if (!payment.BankResponse.IsValid())
                return payment;

            await this.RecordPayment(payment);

            return payment;
        }

        private bool IsPaymentValid(Payment payment)
        {
            return payment != null && payment.IsValid();
        }

        private Task RecordPayment(Payment payment)
        {
            payment.CreatedAt = DateTime.Now;
            payment.UpdatedAt = DateTime.Now;

            return this.paymentRepository.InsertAsync(payment);
        }
    }
}