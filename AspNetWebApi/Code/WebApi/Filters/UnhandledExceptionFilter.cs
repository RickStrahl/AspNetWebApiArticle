using System;
using System.Web.Http.Filters;
using AspNetWebApi.Controllers;
using System.Net;

using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using System.Web.Http;

#if true
namespace AspNetWebApi
{
    public class UnhandledExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext
                                         context)
        {
            HttpStatusCode status = HttpStatusCode.InternalServerError;

            var exType = context.Exception.GetType();

            if (exType == typeof(UnauthorizedAccessException))
                status = HttpStatusCode.Unauthorized;
            else if (exType == typeof(ArgumentException))
                status = HttpStatusCode.NotFound;

            var apiError = new ApiMessageError() { message = context.Exception.Message };


            var errMsg = context.Request.CreateResponse<ApiMessageError>(status,apiError);                                                                         

            context.Result = errMsg;
            base.OnException(context);
        }
    }
}
#endif