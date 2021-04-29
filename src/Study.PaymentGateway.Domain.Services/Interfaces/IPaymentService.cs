using System.Threading.Tasks;
using Study.PaymentGateway.Domain.Entities.Payments;

namespace Study.PaymentGateway.Domain.Services.Interfaces
{
    public interface IPaymentService
    {
        /// <summary>
        ///  Evaluate Payment and indicate if the Purchase is accept by Financial Institution.
        /// </summary>
        /// <param name="payment">Payment</param>
        /// <returns>True : Valid, False : Invalid</returns>
        Task<Payment> ProcessPaymentAsync(Payment payment);
    }
}