// this code requires a reference to JSON.NET in your project
#if true

using System;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Json;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Converters;

namespace Westwind.Web.WebApi
{
    public class JsonNetFormatter : MediaTypeFormatter
    {
        public JsonNetFormatter()
        {
            SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
        }

        protected override bool CanWriteType(Type type)
        {
            // don't serialize JsonValue structure use default for that
            if (type == typeof(JsonValue) || type == typeof(JsonObject) || type == typeof(JsonArray))
                return false;

            return true;
        }

        protected override bool CanReadType(Type type)
        {
            if (type == typeof(IKeyValueModel))
                return false;

            return true;
        }

        protected override System.Threading.Tasks.Task<object> OnReadFromStreamAsync(Type type, System.IO.Stream stream, System.Net.Http.Headers.HttpContentHeaders contentHeaders, FormatterContext formatterContext)
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

        protected override System.Threading.Tasks.Task OnWriteToStreamAsync(Type type, object value, System.IO.Stream stream, System.Net.Http.Headers.HttpContentHeaders contentHeaders, FormatterContext formatterContext, System.Net.TransportContext transportContext)
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
#endif