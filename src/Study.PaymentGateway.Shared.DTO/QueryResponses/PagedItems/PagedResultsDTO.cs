using System.Collections.Generic;

namespace Study.PaymentGateway.Shared.DTO.QueryResponses.PagedItems
{
    public class PagedResultsDTO<T> where T : class
    {
        public PagedResultsDTO()
        {
            Records = new List<T>();
        }

        public long TotalItems { get; set; }
        public int pageTotal;

        public int PageTotal
        {
            get { return Records.Count; }
            set { pageTotal = value; }
        }

        public List<T> Records { get; set; }

        public List<string> Messages()
        {
            List<string> messages = new List<string>();

            if (this.Records.Count == 0)
            {
                messages.Add("No Results");
            }

            return messages;
        }
    }
}