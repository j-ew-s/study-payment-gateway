namespace Study.PaymentGateway.API.Controllers.v1.Payments
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Study.PaymentGateway.API.Controllers.Base;
    using Study.PaymentGateway.App.Services.Interfaces;
    using Study.PaymentGateway.Shared.DTO.Payments;

    [Route("api/payments")]
    [ApiController]
    public class PaymentsController : BaseController
    {
        private IPaymentAppService paymentAppService;

        public PaymentsController(IPaymentAppService paymentAppService)
        {
            this.paymentAppService = paymentAppService;
        }

        /// <summary>
        /// Process a Payment.
        /// </summary>
        /// <param name="paymentDto">PAYMENT Object</param>
        /// <returns>Response Obj</returns>
        [HttpPost]
        public async Task<IActionResult> ProcessPayment(PaymentDTO paymentDto)
        {
            var result = await this.paymentAppService.ProcessPaymentAsync(paymentDto);

            var locationName = nameof(ProcessPayment);
            var responseObject = new
            {
                id = result.Response.MerchantId,
                paymentId = result.Response.Id
            };

            return ProcessResponse(locationName, responseObject, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await this.paymentAppService.GetByIdAsync(id);

            return ProcessResponse(result);
        }
    }
}