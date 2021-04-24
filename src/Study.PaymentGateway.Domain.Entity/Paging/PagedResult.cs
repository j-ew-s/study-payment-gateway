using System.Collections.Generic;

namespace Study.PaymentGateway.Domain.Entities.Paging
{
    public class PagedResult<T> where T : class
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