using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;

using MusicAlbums;
using System.Net;
using System.Xml;

namespace AspNetWebApi.Controllers
{
    /// <summary>
    /// Sample API Controller that demonstrates various different
    /// kinds of data types that can be passed and returned
    /// </summary>
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




        [HttpGet]
        public HttpResponseMessage ReturnAlbum(string title)
        {
            var album = new Album();
            var resp = Request.CreateResponse<Album>(HttpStatusCode.OK, album);
            return resp;
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


        [HttpGet]
        public string ReturnRequestBody(HttpRequestMessage request)
        {
            return request.Content.ReadAsStringAsync().Result;
        }

        [HttpPost]
        public string ReturnXmlDocument(HttpRequestMessage request)
        {
            var doc = new XmlDocument();
            doc.Load(request.Content.ReadAsStreamAsync().Result);
            return doc.DocumentElement.OuterXml;
        }

    }
}