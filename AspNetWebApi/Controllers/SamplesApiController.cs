using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;

using MusicAlbums;
using System.Net;
using System.Xml;
using Newtonsoft.Json.Linq;
using System.Web.Security;
using System.Web;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetWebApi
{
    /// <summary>
    /// Sample API Controller that demonstrates various different
    /// kinds of data types that can be passed and returned
    /// </summary>    
    //[UnhandledExceptionFilter]
    public class SamplesApiController : ApiController
    {


        [HttpGet]
        public void ThrowException()
        {
            throw new UnauthorizedAccessException("Unauthorized Access Sucka");
        }

        [HttpGet]
        public void ThrowError()
        {
            var resp = Request.CreateResponse<ApiMessageError>(
                    HttpStatusCode.BadRequest,
                    new ApiMessageError("Your code stinks!"));
            throw new HttpResponseException(resp);
        }



        /// <summary>
        /// Using CreateErrorResponse native in Web API produces
        /// an exception object to the client
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage ThrowHttpError()
        {
            var ex = new ApplicationException("Whoa, hold on an application Error Occurred");
            ex.Source = "Source code";
            ///ex.StackTrace = "Stack Trace";

            //return Request.CreateErrorResponse(HttpStatusCode.BadRequest, 
            //                                   "Baaaaad Code");
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
        }


        [HttpGet]
        public void ThrowErrorSafe()
        {
            try
            {
                List<string> list = null;
                list.Add("Rick");
            }
            catch (Exception ex)
            {
                ThrowSafeException(ex.Message);
            }
        }


        private void ThrowSafeException(string message,
                    HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            var errResponse = Request.CreateResponse<ApiMessageError>(statusCode,
                                        new ApiMessageError() { message = message });

            throw new HttpResponseException(errResponse);
        }

        [HttpGet]
        public string ReturnAlbumInfo([FromBody] Album album)
        {
            return album.AlbumName + " (" + album.YearReleased.ToString() + ")";
        }

        //public string  ReturnString([FromBody] string message)
        public string ReturnFormVariableString(FormDataCollection formData)
        {
            return formData.Get("message");
        }

        [HttpGet]
        public HttpResponseMessage ReturnDateTime([FromBody] DateTime time)
        {
            return Request.CreateResponse<DateTime>(HttpStatusCode.OK, time);
        }

        [HttpGet]
        public string ReturnMessageModel(MessageModel model)
        {
            return model.Message;
        }
        public class MessageModel
        {
            public string Message { get; set; }
        }



        /// <summary>
        /// Test Forms Authentication based login
        /// Don't do this unless it's over SSL and use POST not GET
        /// Done to test that FormsAuth cookies are sent and retrieved
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost, HttpGet]
        public HttpResponseMessage Authenticate(LoginData login)
        {
            //var username = form.Get("Username");
            //var password = form.Get("Password");

            string username = login.Username;
            string password = login.Password;

            bool authenticated = FormsAuthentication.Authenticate(username, password);
            var cookie = FormsAuthentication.GetAuthCookie(username, false);

            if (!authenticated)
                return Request.CreateResponse<bool>(HttpStatusCode.Unauthorized, false);

            var response = Request.CreateResponse<bool>(HttpStatusCode.OK, true);
            response.Headers.AddCookies(new CookieHeaderValue[1] { new CookieHeaderValue(cookie.Name, cookie.Value) });

            return response;
        }

        /// <summary>
        /// Verify whether Authenticate worked. If Authenticate was successful
        /// IsAuthenticated should return true on subsequent request assuming
        /// client passes the auth cookie (ie. XHR in browser, or Http client 
        /// that explicitly forwards cookie headers).
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public bool IsAuthenticated()
        {
            if (User.Identity.IsAuthenticated)
                return true;

            return false;
        }


        /// <summary>
        /// Returning the request body as a string
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public string ReturnRequestBody(HttpRequestMessage request)
        {
            return request.Content.ReadAsStringAsync().Result;
        }

        /// <summary>
        /// Returning request body of an XML document as a string
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public string ReturnXmlDocument(HttpRequestMessage request)
        {
            var doc = new XmlDocument();
            doc.Load(request.Content.ReadAsStreamAsync().Result);
            return doc.DocumentElement.OuterXml;
        }

        /// <summary>
        /// Pass in an arbitrary object and parse
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost, HttpGet]
        public string JsonValue(JObject value)
        {
            // Dynamically parse json object
            dynamic dval = value;

            string val = dval.Id;

            Album album = new Album()
            {
                Id = dval.Id,
                Entered = dval.Entered
            };

            return String.Format("{0} {1:d}", album.Id, album.Entered);
        }

        /// <summary>
        /// Demonstrates passing multiple parameters into a method
        /// </summary>
        /// <param name="album"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public string PostAlbumDynamicJson(JObject jsonData)
        {
            dynamic json = jsonData;
            JObject jalbum = json.Album;
            JObject juser = json.User;
            string token = json.UserToken;

            var album = jalbum.ToObject<Album>();
            var user = juser.ToObject<User>();

            return String.Format("{0} {1} {2}", album.AlbumName, user.Name, token);
        }

        [HttpPost]
        public string PostAlbumForm(FormDataCollection form)
        {
            return string.Format("{0} - released {1}",
                                    form.Get("AlbumName"), form.Get("RearReleased"));
        }

        /// <summary>
        /// Example of passing multiple parameters. Note userToken must be
        /// passed as a query string value.
        /// </summary>
        /// <param name="album"></param>
        /// <param name="userToken"></param>
        /// <returns></returns>
        [HttpPost]
        public string PostAlbum(Album album, string userToken)
        {

            return String.Format("{0} {1:d} {2}", album.AlbumName, album.Entered, userToken);
        }

        /// <summary>
        /// Demonstrates using a Request and Response Parameter wrappers
        /// to provide a full type for model or value wrapping.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public PostAlbumResponse PostAlbum(PostAlbumRequest request)
        {
            var album = request.Album;
            var userToken = request.UserToken;

            return new PostAlbumResponse()
            {
                IsSuccess = true,
                Result = String.Format("{0} {1:d} {2}", album.AlbumName, album.Entered, userToken)
            };
        }

        [HttpPost]
        public JObject PostAlbumJObject(JObject jAlbum)
        {
            // dynamic input from inbound JSON
            dynamic album = jAlbum;

            // create a new JSON object to write out
            dynamic newAlbum = new JObject();

            // Create properties on the new instance
            // with values from the first
            newAlbum.AlbumName = album.AlbumName + " New";
            newAlbum.NewProperty = "something new";
            newAlbum.Songs = new JArray();

            foreach (dynamic song in album.Songs)
            {
                song.SongName = song.SongName + " New";
                newAlbum.Songs.Add(song);
            }

            return newAlbum;
        }

        [HttpPost, HttpGet]
        public string PostMultipleSimpleValues(string name, int value, DateTime entered, string action = null)
        {
            return string.Format("Name: {0}, Value: {1}, Date: {2}, Action: {3}", name, value, entered, action);
        }

        [HttpPost, HttpGet]
        public string PostMultipleSimpleValuesJson(NameValueEntered nve, string action = null)
        {
            return string.Format("Name: {0}, Value: {1}, Date: {2}, Action: {3}", nve.name, nve.value, nve.entered, action);
        }

        [HttpPost]
        public Task<string> PostFileValues()
        {
            string root = HttpContext.Current.Server.MapPath("~/");
            var provider = new MultipartFormDataStreamProvider(root);

            Task<string> res = Request.Content
                    .ReadAsMultipartAsync(provider)
                    .ContinueWith(itask =>
                    {
                        var count = provider.FileData.Count;

                        // ... do something with files                        
                        return count + " files";
                    });

            return res;
        }

        [HttpPost]
        public string PostRawBuffer([NakedBody] string raw)
        {
            return raw;
        }

        [HttpPost]
        public string PostBinaryBuffer([NakedBody] byte[] raw)
        {
            return raw.Length + " bytes sent";
        }

        [HttpPost]
        public string PostJsonString([FromBody] string raw)
        {
            return raw;
        }

        [HttpPost]
        public int PostJsonNumber([FromBody] int value)
        {
            return value;
        }

        [HttpPost]
        public async Task<string> PostRawBufferManual()
        {
            string result = await Request.Content.ReadAsStringAsync();
            return result;
        }
    }

    public class LoginData
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class RawBuffer
    {
        public string Data { get; set; }
    }

    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserToken { get; set; }
    }

    public class PostAlbumRequest
    {
        public Album Album { get; set; }
        public User User { get; set; }
        public string UserToken { get; set; }
    }

    public class PostAlbumResponse
    {
        public string Result { get; set; }
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class NameValueEntered
    {
        public string name { get; set; }
        public int value { get; set; }
        public DateTime entered { get; set; }
    }
}
