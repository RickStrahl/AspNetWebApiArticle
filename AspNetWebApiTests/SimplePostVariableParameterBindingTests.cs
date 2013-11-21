using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.IO;
using System.Net.Http.Headers;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Text;

namespace AspNetWebApiTests
{
    [TestClass]
    public class SimplePostVariableParameterBindingTests
    {

        private string BaseUrl = ConfigurationManager.AppSettings["BaseUrl"];

        [TestMethod]
        public async Task SimplePostValuesTest()
        {
            var http = new HttpClient();

            var nvc = new Dictionary<string,string>();
            nvc.Add("name", "rick");
            nvc.Add("value", "10");
            nvc.Add("entered", "12/10/2012");
            var ue = new FormUrlEncodedContent(nvc);

            var response = await http.PostAsync(BaseUrl + "samples/PostMultipleSimpleValues?action=json", ue);

            Assert.IsTrue(response.IsSuccessStatusCode);

            string result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result);
        }

        [TestMethod]
        public async Task SimplePostJsonValuesTest()
        {
            var http = new HttpClient();

            var jobject = new JObject();
            dynamic jo = jobject;
            jo.name = "Rick";
            jo.value = 10;
            jo.entered = DateTime.Now.AddDays(-100);

            var content = new StringContent(jobject.ToString(),Encoding.UTF8,"application/json");

            var response = await http.PostAsync(BaseUrl + "samples/PostMultipleSimpleValuesJson?action=json", content);

            Assert.IsTrue(response.IsSuccessStatusCode);

            string result = await response.Content.ReadAsStringAsync();

            Console.WriteLine(result);
        }

        [TestMethod]
        public async Task TestFileUpload()
        {
            var http = new HttpClient();

            byte[] data = File.ReadAllBytes(HttpContext.Current.Server.MapPath("~/images/sailbig.jpg"));
            var fileContent = new ByteArrayContent(data);

            fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = "sailbig.jpg",
                Name="file",
             
            };
            var mp = new MultipartContent();
            mp.Add(fileContent);

            var response = await http.PostAsync(BaseUrl + "samples/PostFileValues", mp);

            Assert.IsTrue(response.IsSuccessStatusCode);

            var result = await response.Content.ReadAsStringAsync();

            Console.WriteLine(result);
        }
    }
}
