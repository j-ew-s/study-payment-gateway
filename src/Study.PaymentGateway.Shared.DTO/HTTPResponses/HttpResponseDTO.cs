using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Study.PaymentGateway.Shared.DTO.HTTPResponses
{
    public class HttpResponseDTO<T> where T : class
    {
        public HttpResponseDTO()
        {
            ErrorMessages = new List<string>();
        }

        public T Response { get; set; }
        public List<string> ErrorMessages { get; set; }

        [JsonIgnore]
        public int Status { get; set; }
    }
}