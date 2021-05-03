using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Study.PaymentGateway.App.Services.Factories;
using Study.PaymentGateway.App.Services.Factories.Enums;
using Study.PaymentGateway.App.Services.Interfaces;
using Study.PaymentGateway.Domain.Entities.Payments;
using Study.PaymentGateway.Domain.Services.Interfaces;
using Study.PaymentGateway.Shared.DTO.HTTPResponses;
using Study.PaymentGateway.Shared.DTO.Payments;
using Study.PaymentGateway.Shared.DTO.QueryResponses.PagedItems;

namespace Study.PaymentGateway.App.Services
{
    public class PaymentAppService : IPaymentAppService
    {
        private readonly IPaymentService paymentService;
        private readonly IMapper mapper;

        public PaymentAppService(IPaymentService paymentService, IMapper mapper)
        {
            this.paymentService = paymentService;
            this.mapper = mapper;
        }

        public async Task<HttpResponseDTO<PaymentDTO>> GetByIdAsync(Guid id)
        {
            var payment = await this.paymentService.GetByIdAsync(id);

            var paymentDto = this.mapper.Map<PaymentDTO>(payment);

            return HttpResponseFactory.Create(paymentDto, payment.ErrorMessages, HttpActionEnum.Get);
        }

        public async Task<HttpResponseDTO<PagedResultDTO<PaymentDTO>>> GetPaymentByCardNumberAsync(long cardNumber, int currentPage, int itemsPerPage)
        {
            var pagedResult = await this.paymentService.GetPaymentByCardNumberAsync(cardNumber, currentPage, itemsPerPage);

            var pagedResultDto = this.mapper.Map<PagedResultDTO<PaymentDTO>>(pagedResult);

            var errorMessages = pagedResult.Records
                .Where(w => w.ErrorMessages.Any())
                .SelectMany(s => s.ErrorMessages)
                .ToList();

            return HttpResponseFactory.Create(pagedResultDto, errorMessages, HttpActionEnum.GetQueryString);
        }

        public async Task<HttpResponseDTO<List<PaymentDTO>>> GetPaymentByMerchantIdAsync(Guid id)
        {
            var payment = await this.paymentService.GetPaymentByMerchantIdAsync(id);

            var paymentDto = this.mapper.Map<List<PaymentDTO>>(payment);

            var errorMessages = payment
               .Where(w => w.ErrorMessages.Any())
               .SelectMany(s => s.ErrorMessages)
               .ToList();

            return HttpResponseFactory.Create(paymentDto, errorMessages, HttpActionEnum.Get);
        }

        public async Task<HttpResponseDTO<PaymentDTO>> ProcessPaymentAsync(PaymentDTO paymentDto)
        {
            var payment = this.mapper.Map<Payment>(paymentDto);

            payment = await this.paymentService.ProcessPaymentAsync(payment);

            paymentDto = this.mapper.Map<PaymentDTO>(payment);

            return HttpResponseFactory.Create(paymentDto, payment.ErrorMessages, HttpActionEnum.Insert);
        }
    }
}