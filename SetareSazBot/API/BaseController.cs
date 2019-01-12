using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using SetareSazBot.API.Json.Output;
using SetareSazBot.Domain.Enum;

namespace SetareSazBot.API
{
    public class BaseController : ApiController
    {
        protected IHttpActionResult CustomResult(object result = null)
        {
            return Ok(new BaseJson
            {
                Status = Domain.Enum.StatusCode.Ok,
                Result = result
            });
        }

        protected IHttpActionResult CustomError()
        {
            return CustomError(HttpStatusCode.BadRequest, null, Domain.Enum.StatusCode.UserError, null);
        }

        protected IHttpActionResult CustomError(string error)
        {
            return CustomError(HttpStatusCode.BadRequest, error, Domain.Enum.StatusCode.UserError, null);
        }

        protected IHttpActionResult CustomError(object result, StatusCode statusCode)
        {
            return CustomError(HttpStatusCode.BadRequest, null, statusCode, result);
        }

        protected IHttpActionResult CustomError(Exception exception)
        {
            return CustomError(HttpStatusCode.InternalServerError, exception.ToString(), Domain.Enum.StatusCode.ApplicationError, null);
        }

        protected IHttpActionResult CustomError(ModelStateDictionary modelState)
        {
            var error = string.Join(" ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(modelError => modelError.ErrorMessage).ToArray());

            return CustomError(HttpStatusCode.BadRequest, error, Domain.Enum.StatusCode.UserError, null);
        }

        private IHttpActionResult CustomError(HttpStatusCode httpStatusCode, string error, StatusCode statusCode, object result)
        {
            return Content(httpStatusCode, new BaseJson
            {
                Status = statusCode,
                Result = result,
                Message = error
            });
        }
    }
}
