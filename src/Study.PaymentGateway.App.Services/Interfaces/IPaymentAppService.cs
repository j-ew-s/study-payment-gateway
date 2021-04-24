using System.Threading.Tasks;
using Study.PaymentGateway.Shared.DTO.HTTPResponses;
using Study.PaymentGateway.Shared.DTO.Payments;

namespace Study.PaymentGateway.App.Services.Interfaces
{
    public interface IPaymentAppService
    {
        Task<HttpResponseDTO<PaymentDTO>> ProcessPayment(PaymentDTO paymentDto);
    }
}