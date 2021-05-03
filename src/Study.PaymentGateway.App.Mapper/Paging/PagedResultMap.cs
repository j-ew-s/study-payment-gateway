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
            CreateMap<PagedResult<Payment>, PagedResultDTO<PaymentDTO>>();
            CreateMap<PagedResultDTO<PaymentDTO>, PagedResult<Payment>>();
        }
    }
}