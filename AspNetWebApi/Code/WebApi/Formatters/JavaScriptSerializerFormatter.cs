using System;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.IO;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;

namespace Westwind.Web.WebApi
{
    public class JavaScriptSerializerFormatter : MediaTypeFormatter
    {
        public JavaScriptSerializerFormatter()
        {
            SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
        }

        public override bool CanWriteType(Type type)
        {
            // don't serialize JsonValue structure use default for that
            if (type == typeof(JValue) || type == typeof(JObject) || type== typeof(JArray) )
                return false;
           
            return true;
        }

        public override bool CanReadType(Type type)
        {
            return true;
        }

        public override System.Threading.Tasks.Task<object> ReadFromStreamAsync(Type type, System.IO.Stream stream, 
                                                                                   HttpContentHeaders contentHeaders, 
                                                                                   IFormatterLogger formatterLogger)
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


        public override System.Threading.Tasks.Task WriteToStreamAsync(Type type, object value, 
                                                                          System.IO.Stream stream, 
                                                                          HttpContentHeaders contentHeaders,                                                                           
                                                                          System.Net.TransportContext transportContext)
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