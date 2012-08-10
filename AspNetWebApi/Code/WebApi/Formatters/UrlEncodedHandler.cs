using System;
using System.IO;
using System.Net;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Net.Http;

namespace Westwind.Web.WebApi
{

    /// <summary>
    /// Handles JsonP requests when requests are fired with text/javascript
    /// </summary>
    public class UrlEncodedFormatter : MediaTypeFormatter
    {                

        public UrlEncodedFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/x-www-form-urlencoded"));            
            
            //MediaTypeMappings.Add(new UriPathExtensionMapping("jsonp", "application/json"));
            
        }


        public override bool CanWriteType(Type type)
        {
            return true;
        }

        public override bool CanReadType(Type type)
        {
            return false;
        }

        /// <summary>
        /// Override this method to capture the Request object
        /// </summary>
        /// <param name="type"></param>
        /// <param name="request"></param>
        /// <param name="mediaType"></param>
        /// <returns></returns>
        public override MediaTypeFormatter GetPerRequestFormatterInstance(Type type, System.Net.Http.HttpRequestMessage request, MediaTypeHeaderValue mediaType)
        {
            var formatter = new UrlEncodedFormatter(); 
            {                 
            };
            

            return formatter;
        }

 
        public override Task WriteToStreamAsync(Type type, object value, 
                                        Stream stream, 
                                        HttpContent content, 
                                        TransportContext transportContext)
        {                                     
             return base.WriteToStreamAsync(type, value, stream, content, transportContext);            
        }
       
    }
}