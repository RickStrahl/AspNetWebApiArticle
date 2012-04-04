using System;
using System.Collections.Generic;
using System.Linq;

namespace AspNetWebApi
{
    /// <summary>
    /// Class that represents an error returned to
    /// the client. Can be explicitly returned or
    /// as part of the UnhandledExceptionFilter
    /// </summary>
    public class ApiMessageError
    {
        public string message { get; set; }
        public bool isCallbackError { get; set; }
        public List<string> errors { get; set; }

        public ApiMessageError()
            : this(null)
        {
        }
                
        public ApiMessageError(string errorMessage)
        {
            isCallbackError = true;
            errors = new List<string>();
            message = errorMessage;
        }
    }
}
