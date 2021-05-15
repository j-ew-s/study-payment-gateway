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
        Task<HttpResponseDTO<PaymentResponseDTO>> ProcessPaymentAsync(PaymentDTO paymentDto);

        Task<HttpResponseDTO<PaymentResponseDTO>> GetByIdAsync(Guid id);

        Task<HttpResponseDTO<List<PaymentResponseDTO>>> GetPaymentByMerchantIdAsync(Guid id);

        Task<HttpResponseDTO<PagedResultsDTO<PaymentResponseDTO>>> GetPaymentByCardNumberAsync(long cardNumber, int currentPage, int itemsPerPage);
    }
}