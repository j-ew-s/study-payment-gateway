using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Study.PaymentGateway.Shared.DTO.HTTPResponses;
using Study.PaymentGateway.Shared.DTO.Payments;
using Study.PaymentGateway.Shared.DTO.QueryResponses.PagedItems;

namespace Study.PaymentGateway.App.Services.Interfaces
{
    public interface IPaymentAppService
    {
        Task<HttpResponseDTO<PaymentDTO>> ProcessPaymentAsync(PaymentDTO paymentDto);

        Task<HttpResponseDTO<PaymentDTO>> GetByIdAsync(Guid id);

        Task<HttpResponseDTO<List<PaymentDTO>>> GetPaymentByMerchantIdAsync(Guid id);

        Task<HttpResponseDTO<PagedResultDTO<PaymentDTO>>> GetPaymentByCardNumberAsync(long cardNumber, int currentPage, int itemsPerPage);
    }
}