using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Study.PaymentGateway.API.Controllers.Base;
using Study.PaymentGateway.App.Services.Interfaces;
using Study.PaymentGateway.Shared.DTO.Payments;

namespace Study.PaymentGateway.API.Controllers.v1.Merchant
{
    [Route("api/v1/merchants")]
    [ApiController]
    public class MerchantController : BaseController
    {
        private IPaymentAppService paymentAppService;

        public MerchantController(IPaymentAppService paymentAppService)
        {
            this.paymentAppService = paymentAppService;
        }

        // POST api/<MerchantController>
        [HttpPost]
        public async Task<IActionResult> ProcessPayment([FromBody] PaymentDTO payment)
        {
        }

        [HttpGet("{id}/payments/{paymentId}")]
        public async Task<IActionResult> GetPayment(Guid id, Guid paymentId)
        {
            return null;
        }
    }
}