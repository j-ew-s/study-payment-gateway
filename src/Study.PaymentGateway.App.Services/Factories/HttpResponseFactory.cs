using System.Collections.Generic;
using Study.PaymentGateway.App.Services.Factories.Enums;
using Study.PaymentGateway.Shared.DTO.HTTPResponses;

namespace Study.PaymentGateway.App.Services.Factories
{
    public static class HttpResponseFactory
    {
        /// <summary>
        /// Creates a new HttpResponseDto object
        /// </summary>
        /// <typeparam name="T">Class that are from type Validation</typeparam>
        /// <param name="entity">Object Instance of T</param>
        /// <returns>HttpResponseDTO object</returns>
        public static HttpResponseDTO<T> Create<T>(T entity, IReadOnlyList<string> messages, HttpActionEnum httpAction) where T : class
        {
            var httpResponse = new HttpResponseDTO<T>();

            httpResponse.Status = GetStatus(messages.Count, httpAction);
            httpResponse.ErrorMessages.AddRange(GetMessage(messages, httpAction));
            httpResponse.Response = entity;

            return httpResponse;
        }

        private static int GetStatus(int messages, HttpActionEnum httpAction)
        {
            var messageQuantity = messages;

            switch (httpAction)
            {
                case HttpActionEnum.Insert:
                    return EvaluateInsert(messageQuantity);

                case HttpActionEnum.Delete:
                    return EvaluateDelete(messageQuantity);

                case HttpActionEnum.Get:
                    return EvaluateGet(messageQuantity);

                case HttpActionEnum.Update:
                    return EvaluateUpdate(messageQuantity);

                case HttpActionEnum.GetQueryString:
                    return EvaluateGetQueryString(messageQuantity);

                case HttpActionEnum.Exception:
                    return 500;

                default:
                    return 200;
            }
        }

        private static IReadOnlyList<string> GetMessage(IReadOnlyList<string> messages, HttpActionEnum httpAction)
        {
            var messageQuantity = messages.Count;

            switch (httpAction)
            {
                case HttpActionEnum.Get:
                case HttpActionEnum.GetQueryString:
                    return messageQuantity > 0 ? new List<string>() { "Not Found" } : messages;

                default:
                    return messages;
            }
        }

        private static int EvaluateInsert(int messageQuantity)
        {
            if (messageQuantity == 0)
                return 201;
            else
                return 400;
        }

        private static int EvaluateUpdate(int messageQuantity)
        {
            return BaseEvaluate(messageQuantity);
        }

        private static int EvaluateGet(int messageQuantity)
        {
            if (messageQuantity == 0)
                return 200;
            else
                return 404;
        }

        private static int EvaluateDelete(int messageQuantity)
        {
            return BaseEvaluate(messageQuantity);
        }

        private static int EvaluateGetQueryString(int messageQuantity)
        {
            if (messageQuantity == 0)
                return 200;
            else
                return 204;
        }

        private static int BaseEvaluate(int messageQuantity)
        {
            if (messageQuantity == 0)
                return 200;
            else
                return 400;
        }
    }
}