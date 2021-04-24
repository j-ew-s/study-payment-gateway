using System.Net;
using Microsoft.AspNetCore.Mvc;
using Study.PaymentGateway.Shared.DTO.HTTPResponses;

namespace Study.PaymentGateway.API.Controllers.Base
{
    public class BaseController : ControllerBase
    {
        public IActionResult ProcessResponse<T>(string actionName, object responseValue, HttpResponseDTO<T> input) where T : class
        {
            return CreateProcessResponse(actionName, responseValue, input);
        }

        public IActionResult ProcessResponse<T>(HttpResponseDTO<T> input) where T : class
        {
            return CreateProcessResponse(null, null, input);
        }

        private IActionResult CreateProcessResponse<T>(string actionName, object responseValue, HttpResponseDTO<T> input) where T : class
        {
            if (input == null)
            {
                return StatusCode(500);
            }

            switch ((HttpStatusCode)input.Status)
            {
                case HttpStatusCode.Created:
                    return CreatedAtAction(actionName, responseValue, input);

                case HttpStatusCode.NoContent:
                    return NoContent();

                case HttpStatusCode.NotFound:
                    return NotFound(input);

                case HttpStatusCode.BadRequest:
                    return BadRequest(input);

                case HttpStatusCode.OK:
                default:
                    return Ok(input);
            }
        }
    }
}