using System.Threading.Tasks;
using AutoMapper;
using Study.PaymentGateway.App.Services.Factories;
using Study.PaymentGateway.App.Services.Factories.Enums;
using Study.PaymentGateway.App.Services.Interfaces;
using Study.PaymentGateway.Domain.Entities.Payments;
using Study.PaymentGateway.Domain.Services.Interfaces;
using Study.PaymentGateway.Shared.DTO.HTTPResponses;
using Study.PaymentGateway.Shared.DTO.Payments;

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

        public async Task<HttpResponseDTO<PaymentDTO>> ProcessPayment(PaymentDTO paymentDto)
        {
            var payment = this.mapper.Map<Payment>(paymentDto);

            if (!payment.IsValid())
            {
                return HttpResponseFactory.Create(paymentDto, payment.ErrorMessages, HttpActionEnum.Insert);
            }

            payment = await this.paymentService.ProcessPayment(payment);

            paymentDto = this.mapper.Map<PaymentDTO>(payment);

            return HttpResponseFactory.Create(paymentDto, payment.ErrorMessages, HttpActionEnum.Insert);
        }
    }
}