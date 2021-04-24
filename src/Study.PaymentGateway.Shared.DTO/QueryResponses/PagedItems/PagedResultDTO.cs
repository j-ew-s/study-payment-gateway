using System.Collections.Generic;

namespace Study.PaymentGateway.Shared.DTO.QueryResponses.PagedItems
{
    public class PagedResultDTO<T> where T : class
    {
        public long TotalItems { get; set; }
        public int pageTotal;

        public int PageTotal
        {
            get { return Records.Count; }
            set { pageTotal = value; }
        }

        public List<T> Records { get; set; }
    }
}