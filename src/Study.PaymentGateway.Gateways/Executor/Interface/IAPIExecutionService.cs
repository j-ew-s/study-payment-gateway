using System.Threading.Tasks;

namespace Study.PaymentGateway.Gateways.Executor.Interface
{
    public interface IAPIExecutionService
    {
        Task<T> Get<T>(string a) where T : class;

        Task<T> Post<T>(string a) where T : class;
    }
}