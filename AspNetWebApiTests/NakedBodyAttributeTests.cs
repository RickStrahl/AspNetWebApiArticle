using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AspNetWebApiTests
{
    /// <summary>
    /// Summary description for NakedBodyAttributeTests
    /// </summary>
    [TestClass]
    public class NakedBodyAttributeTests
    {
        private string BaseUrl = ConfigurationManager.AppSettings["BaseUrl"];

        [TestMethod]
        public async Task NakedBodyStringTest()
        {
            string url = BaseUrl + "samples/PostRawBuffer";

            // posting a plain string - non-json
            string post = "This is a raw string buffer...";

            var httpClient = new HttpClient();
            var content = new StringContent(post);
            var response = await httpClient.PostAsync(url, content);

            Assert.IsTrue(response.IsSuccessStatusCode, "Error code: " + response.StatusCode.ToString());

            string result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result);

            // result is a JSON response (default output)
            Assert.AreEqual(result, "\"" + post + "\"");
        }

        [TestMethod]
        public async Task ManualBodyStringTest()
        {
            string url = BaseUrl + "samples/PostRawBufferManual";

            // posting a plain string - non-json
            string post = "Hello Manual World.";

            var httpClient = new HttpClient();

            var content = new StringContent(post, Encoding.UTF8);
            var response = await httpClient.PostAsync(url, content);

            Assert.IsTrue(response.IsSuccessStatusCode, "Error code: " + response.StatusCode.ToString());

            string result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result);

            // result is a JSON response (default output)
            Assert.AreEqual(result, "\"" + post + "\"");
        }


        [TestMethod]
        public async Task JsonStringStringTest()
        {
            string url = BaseUrl + "samples/PostJsonString";

            // posting a JSON string (encdoed as part of request)
            string postString = "Posting a JSON string.";

            var httpClient = new HttpClient();
            var response = await httpClient.PostAsync<string>(url, postString, new JsonMediaTypeFormatter());

            Assert.IsTrue(response.IsSuccessStatusCode, "Error code: " + response.StatusCode.ToString());

            string result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result);

            // check for a JSON string response 
            Assert.AreEqual("\"" + postString + "\"", result);
        }

        [TestMethod]
        public async Task NakedBodyBinaryTest()
        {
            string url = BaseUrl + "samples/PostBinaryBuffer";
            var postBin = new byte[] { 10, 13, 44, 45, 10, 13 };

            var httpClient = new HttpClient();
            var content = new ByteArrayContent(postBin);
            var response = await httpClient.PostAsync(url, content);

            string result = await response.Content.ReadAsStringAsync();

            Console.WriteLine(result);
        }

        [TestMethod]
        public async Task JsonStringParameterTest()
        {
            string url = BaseUrl + "samples/PostJsonString";

            // send JSON string - requires [FromBody]            
            var post = "\"Hello cruel JSON world\"";

            var httpClient = new HttpClient();

            var content = new StringContent(post, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(url, content);
            var result = await response.Content.ReadAsStringAsync();

            Console.WriteLine(result);

            Assert.AreEqual(result, post);
        }

        [TestMethod]
        public async Task JsonNumberParameterTest()
        {
            string url = BaseUrl + "samples/PostJsonNumber";

            // send JSON string - requires [FromBody]            
            int post = 1;

            var httpClient = new HttpClient();

            var response = await httpClient.PostAsync<int>(url, post, new JsonMediaTypeFormatter());

            var result = await response.Content.ReadAsAsync<int>();

            Console.WriteLine(result);

            Assert.AreEqual(result, post);
        }


    }
}
