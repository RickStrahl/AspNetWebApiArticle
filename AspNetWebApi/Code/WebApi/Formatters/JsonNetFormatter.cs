// this code requires a reference to JSON.NET in your project

using System;
using System.Net.Http.Formatting;
using System.Threading.Tasks;

using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Converters;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net;

namespace Westwind.Web.WebApi
{
    public class JsonNetFormatter : MediaTypeFormatter
    {
        public JsonNetFormatter()
        {
            SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
        }

        public override bool CanWriteType(Type type)
        {
            // don't serialize JsonValue structure use default for that
            if (type == typeof(JValue) || type == typeof(JObject) || type == typeof(JArray))
                return false;

            return true;
        }

        public override bool CanReadType(Type type)
        {
            return true;
        }

        public override System.Threading.Tasks.Task<object> ReadFromStreamAsync(Type type, 
                                                            Stream stream, 
                                                            HttpContent content,
                                                            IFormatterLogger formatterLogger)
        {
            var task = Task<object>.Factory.StartNew(() =>
                {
                    var settings = new JsonSerializerSettings()
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                    };
                   
                    var sr = new StreamReader(stream);
                    var jreader = new JsonTextReader(sr);

                    var ser = new JsonSerializer();
                    ser.Converters.Add(new IsoDateTimeConverter());
                     
                    object val = ser.Deserialize(jreader, type);
                    return val;
                });

            return task;
        }

        public override Task WriteToStreamAsync(Type type, object value, 
                                                Stream stream, 
                                                HttpContent content, 
                                                TransportContext transportContext)
        {            
            var task = Task.Factory.StartNew( () =>
                {                    
                    var settings = new JsonSerializerSettings()
                    {
                         NullValueHandling = NullValueHandling.Ignore,                                                  
                    };
                    
                    string json = JsonConvert.SerializeObject(value, Formatting.Indented, 
                                                              new JsonConverter[1] { new IsoDateTimeConverter() } );                    

                    byte[] buf = System.Text.Encoding.Default.GetBytes(json);
                    stream.Write(buf,0,buf.Length);
                    stream.Flush();
                });

            return task;
        }
    }
}