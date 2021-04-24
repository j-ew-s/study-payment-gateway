using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Study.PaymentGateway.Gateways.Executor.Interface;

namespace Study.PaymentGateway.Gateways.Executor
{
    public class APIExecutionService : IAPIExecutionService
    {
        // Adicionar aqui uma interface para carregar informações dos Bancos da configuração
        public APIExecutionService()
        {
        }

        public async Task<T> Get<T>(string a) where T : class
        {
            var response = await HttpClientExecutor.Get("");

            return await SetResponse<T>(response);
        }

        public async Task<T> Post<T>(string a) where T : class
        {
            var response = await HttpClientExecutor.Get("");

            return await SetResponse<T>(response);
        }

        /// <summary>
        /// Transforms HttpResponseMessage into model's message object
        /// </summary>
        /// <typeparam name="T">Message object type</typeparam>
        /// <param name="response">the HttpResponseMessage </param>
        /// <returns>Parsed HttpResponseMessage into T</returns>
        private async Task<T> SetResponse<T>(HttpResponseMessage response) where T : class
        {
            T entity = null;

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();

                entity = JsonConvert.DeserializeObject<T>(jsonString);
            }
            else
            {
                // TODO : May adding log here
            }

            return entity;
        }
    }
}