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
using System.Globalization;

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

    /// <summary>
    /// Check for simple binding parameters in POST data. Bind POST
    /// data as well as query string data
    /// </summary>
    /// <param name="metadataProvider"></param>
    /// <param name="actionContext"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override Task ExecuteBindingAsync(ModelMetadataProvider metadataProvider,
                                                HttpActionContext actionContext,
                                                CancellationToken cancellationToken)
    {
        // Body can only be read once, so read and cache it
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

        object value = StringToType(stringValue);

        // Set the binding result here
        SetValue(actionContext, value);

        // now, we can return a completed task with no result
        TaskCompletionSource<AsyncVoid> tcs = new TaskCompletionSource<AsyncVoid>();
        tcs.SetResult(default(AsyncVoid));
        return tcs.Task;
    }


    private object StringToType(string stringValue)
    {
        object value = null;

        if (stringValue == null)
            value = null;
        else if (Descriptor.ParameterType == typeof(string))
            value = stringValue;
        else if (Descriptor.ParameterType == typeof(int))
            value = int.Parse(stringValue, CultureInfo.CurrentCulture);
        else if (Descriptor.ParameterType == typeof(Int32))
            value = Int32.Parse(stringValue, CultureInfo.CurrentCulture);
        else if (Descriptor.ParameterType == typeof(Int64))
            value = Int64.Parse(stringValue, CultureInfo.CurrentCulture);
        else if (Descriptor.ParameterType == typeof(decimal))
            value = decimal.Parse(stringValue, CultureInfo.CurrentCulture);
        else if (Descriptor.ParameterType == typeof(double))
            value = double.Parse(stringValue, CultureInfo.CurrentCulture);
        else if (Descriptor.ParameterType == typeof(DateTime))
            value = DateTime.Parse(stringValue, CultureInfo.CurrentCulture);
        else if (Descriptor.ParameterType == typeof(bool))
        {
            value = false;
            if (stringValue == "true" || stringValue == "on" || stringValue == "1")
                value = true;
        }
        else
            value = stringValue;

        return value;
    }

    /// <summary>
    /// Read and cache the request body
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    private NameValueCollection TryReadBody(HttpRequestMessage request)
    {
        object result = null;

        // try to read out of cache first
        if (!request.Properties.TryGetValue(MultipleBodyParameters, out result))
        {
            // parsing the string like firstname=Hongmei&lastname=Ge            
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
