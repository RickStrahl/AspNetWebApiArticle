using System;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Json;
using System.IO;
using System.Collections.Generic;

namespace Westwind.Web.WebApi
{
    public class JavaScriptSerializerFormatter : MediaTypeFormatter
    {
        public JavaScriptSerializerFormatter()
        {
            SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
        }

        protected override bool CanWriteType(Type type)
        {
            // don't serialize JsonValue structure use default for that
            if (type == typeof(JsonValue) || type == typeof(JsonObject) || type== typeof(JsonArray) )
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
                    var ser = new JavaScriptSerializer();                    

                    string json;

                    using (var sr = new StreamReader(stream))
                    {                        
                        json = sr.ReadToEnd();                        
                    }

                    object val = ser.Deserialize(json,type);
                    return val;
                });

            return task;
        }

        protected override System.Threading.Tasks.Task OnWriteToStreamAsync(Type type, object value, System.IO.Stream stream, System.Net.Http.Headers.HttpContentHeaders contentHeaders, FormatterContext formatterContext, System.Net.TransportContext transportContext)
        {            
            var task = Task.Factory.StartNew( () =>
                {
                    var ser = new JavaScriptSerializer();                    
                    var json = ser.Serialize(value);
                    
                    byte[] buf = System.Text.Encoding.Default.GetBytes(json);
                    stream.Write(buf,0,buf.Length);
                    stream.Flush();
                });

            return task;
        }
    }
}