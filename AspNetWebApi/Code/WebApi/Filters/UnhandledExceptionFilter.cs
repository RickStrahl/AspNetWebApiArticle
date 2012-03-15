using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Net.Http;
using AspNetWebApi.Controllers;
using System.Net;

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

        var apiError = new ApiMessageError() 
        { message = context.Exception.Message };

        var errMsg = new HttpResponseMessage<ApiMessageError>(apiError,
                                                              status);

        context.Result = errMsg;
        base.OnException(context);
    }
}
}