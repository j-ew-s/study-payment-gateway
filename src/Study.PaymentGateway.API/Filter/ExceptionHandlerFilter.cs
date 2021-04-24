using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Study.PaymentGateway.App.Services.Factories;
using Study.PaymentGateway.App.Services.Factories.Enums;
using Study.PaymentGateway.Shared.DTO.HTTPResponses;

namespace Study.PaymentGateway.API.Filter
{
    public class ExceptionHandlerFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var httpResponse = CreateHttpResponse(context);

            context.Result = new JsonResult(httpResponse);
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.ExceptionHandled = true;

            base.OnException(context);
        }

        private HttpResponseDTO<object> CreateHttpResponse(ExceptionContext context)
        {
            var allMessages = GetExceptionMessages(context);

            return HttpResponseFactory.Create(new object(), allMessages, HttpActionEnum.Exception);
        }

        private List<string> GetExceptionMessages(ExceptionContext context)
        {
            var innerException = context.Exception;
            List<string> messages = new List<string>();
            do
            {
                if (!string.IsNullOrWhiteSpace(innerException.Message))
                {
                    messages.Add(innerException.Message);
                }

                innerException = innerException.InnerException;
            } while (innerException != null);

            return messages;
        }
    }
}