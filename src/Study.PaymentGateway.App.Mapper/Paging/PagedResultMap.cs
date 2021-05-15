using AutoMapper;
using Study.PaymentGateway.Domain.Entities.Paging;
using Study.PaymentGateway.Domain.Entities.Payments;
using Study.PaymentGateway.Shared.DTO.Payments;
using Study.PaymentGateway.Shared.DTO.QueryResponses.PagedItems;

namespace Study.PaymentGateway.App.Mapper.Paging
{
    public class PagedResultMap : Profile
    {
        public PagedResultMap()
        {
            CreateMap<PagedResults<Payment>, PagedResultsDTO<PaymentDTO>>();
            CreateMap<PagedResultsDTO<PaymentDTO>, PagedResults<Payment>>();
            CreateMap<PagedResultsDTO<PaymentResponseDTO>, PagedResults<Payment>>();
            CreateMap<PagedResults<Payment>, PagedResultsDTO<PaymentResponseDTO>>();
        }
    }
}