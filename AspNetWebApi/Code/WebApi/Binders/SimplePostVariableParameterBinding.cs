using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;
using System.Linq;

namespace Westwind.Web.WebApi
{
    /// <summary>
    /// A Custom HttpParameterBinding to bind multiple parameters from request body
    /// </summary>
    public class SimplePostVariableParameterBinding : HttpParameterBinding
    {
        private const string MultipleBodyParameters = "MultipleBodyParameters";

        public SimplePostVariableParameterBinding(HttpParameterDescriptor descriptor)
            : base(descriptor)
        {
        }

        public override Task ExecuteBindingAsync(ModelMetadataProvider metadataProvider, 
                                                 HttpActionContext actionContext, 
                                                 CancellationToken cancellationToken)
        {
            NameValueCollection col = TryReadBody(actionContext.Request);

            string stringValue = null;
            
            if (col != null)
                stringValue = col[Descriptor.ParameterName];

            // try reading query string if we have no POST/PUT match
            if (stringValue == null)
            {                                
                var query = actionContext.Request.GetQueryNameValuePairs();
                if (query != null)
                {
                    var matches = query.Where(kv => kv.Key.ToLower() == Descriptor.ParameterName.ToLower());
                    if (matches.Count() > 0)
                        stringValue = matches.First().Value;
                }
            }
                            

            object value = null;

            if (stringValue == null)
                value = null;
            else if (Descriptor.ParameterType == typeof(string))
                value = stringValue;
            else if (Descriptor.ParameterType == typeof(int))
                value = int.Parse(stringValue);
            else if (Descriptor.ParameterType == typeof(DateTime))
                value = DateTime.Parse(stringValue);
            else
                value = stringValue;

            // Set the binding result here
            SetValue(actionContext, value);

            // now, we can return a completed task with no result
            TaskCompletionSource<AsyncVoid> tcs = new TaskCompletionSource<AsyncVoid>();
            tcs.SetResult(default(AsyncVoid));
            return tcs.Task;
        }

        NameValueCollection TryReadBody(HttpRequestMessage request)
        {
            object result = null;
            if (!request.Properties.TryGetValue(MultipleBodyParameters, out result))
            {
                // parsing the string like firstname=Hongmei&lastname=Ge
                NameValueCollection collection = new NameValueCollection();
                result = request.Content.ReadAsFormDataAsync().Result;
                request.Properties.Add(MultipleBodyParameters, result);
            }

            return result as NameValueCollection;
        }

        private struct AsyncVoid
        {
        }
    }
}
