using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Study.PaymentGateway.Gateways
{
    public static class HttpClientExecutor
    {
        private static readonly HttpClient client = new HttpClient();

        // Execute GET Commands
        public static async Task<HttpResponseMessage> Get(string uri)
        {
            return await client.GetAsync(uri);
        }

        // Execute POST Commands
        public static async Task<HttpResponseMessage> Post<T>(string uri, T content) where T : class
        {
            var body = PrepareStringContent(content);

            return await client.PostAsync(uri, body);
        }

        /// <summary>
        /// Prepare the Body Content transforming it from Model Object to Json.
        /// </summary>
        /// <typeparam name="T">Model Object Type</typeparam>
        /// <param name="objectToSerialize">Object to be serialized</param>
        /// <returns>StringContent object serialized using UTF-8 encoding with json media type.</returns>
        private static StringContent PrepareStringContent(object objectToSerialize)
        {
            return new StringContent(
                JsonConvert.SerializeObject(objectToSerialize),
                Encoding.UTF8,
                "application/json");
        }
    }
}