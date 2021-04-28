﻿using System.Threading.Tasks;

namespace Study.PaymentGateway.Gateways.Executor.Interface
{
    public interface IAPIExecutionService
    {
        Task<T> Get<T>(string uri) where T : class;

        Task<T> Post<T>(string uri, object content) where T : class;
    }
}