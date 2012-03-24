using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using System.Web.Http;

using MusicAlbums;
using System.Net;
using System.Configuration;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Web;

namespace AspNetWebApi.Controllers
{
    public class AlbumApiController : ApiController
    {
        // sample data - static list
        static List<Album> Albums = Album.CreateSampleAlbumData();


        public IEnumerable<Album> GetAlbums()
        {
            var albums = Albums.OrderBy(alb => alb.Artist);
            return albums;
        }

        public IQueryable<Album> SortableAlbums()
        {
            var albums = Albums.AsQueryable(); //;.OrderBy(alb => alb.Artist).AsQueryable();
            return albums;
        }


        public void ThrowError()
        {
            try
            {
                List<string> list = null;
                list.Add("Rick");

            }
            catch (Exception ex)
            {
                ThrowSafeException(ex.Message);
                ///throw new InvalidOperationException("Bogus excpetion fired");
            }
        }

        public HttpResponseMessage ReturnAlbum(string title)
        {
            var album = new Album();

            var resp = new HttpResponseMessage(HttpStatusCode.OK);

            var formatter = GetDefaultMediaTypeFormatter(typeof(Album));
            resp.Content = new ObjectContent<Album>(album,formatter); 
            //??? HOW DO I GET THE DEFAULT FORMATTER ???);

            return resp;
        }


        private void ThrowSafeException(string message,
                                        HttpStatusCode statusCode =
                                         HttpStatusCode.InternalServerError)
        {
            var errResponse = Request.CreateResponse<ApiMessageError>(statusCode,
                                     new ApiMessageError() { message = message });
                        
            throw new HttpResponseException(errResponse);
        }

public string ReturnAlbumInfo([FromBody] Album album)
{
    return album.AlbumName + " (" + album.YearReleased.ToString() + ")";
}
          
//public string  ReturnString([FromBody] string message)
public string  ReturnString()
{
    var formData = Request.Content.ReadAsAsync<FormDataCollection>().Result;    
    return  formData.Get("message");    
    //return HttpContext.Current.Request.Form["message"]; // "not yet"; //message;
}

public HttpResponseMessage ReturnDateTime([FromBody] DateTime time)
{
    return Request.CreateResponse<DateTime>(HttpStatusCode.OK, time);
}

public string ReturnMessageModel(MessageModel model)
{        
    return model.Message;
}


public class MessageModel
{
    public string Message { get; set; }
}

        /// <summary>
        /// returns the default formatter that Web API
        /// figures out based on its Content Negotiation
        /// </summary>
        /// <returns></returns>
        private MediaTypeFormatter GetDefaultMediaTypeFormatter(Type type)            
        {
            MediaTypeHeaderValue mthv;
            return new DefaultContentNegotiator().Negotiate(type, ControllerContext.Request, ControllerContext.Configuration.Formatters, out mthv);
        }
         


        public Album GetAlbum(string title)
        {
            var album = Albums.Where(alb => alb.AlbumName.Contains(title)).SingleOrDefault();
            return album;
        }


        public HttpResponseMessage AlbumArt(string title)
        {
            var album = GetAlbum(title);
            if (album == null)
            {
                var content = new ObjectContent<ApiMessageError>
                                (new ApiMessageError("Album not found"),null);
                var resp = new HttpResponseMessage()
                    { 
                       Content = content,
                       StatusCode = HttpStatusCode.NotFound
                    };                                
                return  resp;                        
            }

            // kinda silly - we would normally serve this directly                     
            var http = new WebClient();
            var imageData = http.DownloadData(album.AlbumImageUrl);

            var result = new HttpResponseMessage();
            result.Content = new ByteArrayContent(imageData);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");

            return result;
        }


        public HttpResponseMessage PostAlbum(Album album)
        {
            if (!this.ModelState.IsValid)
            {
                // my custom error class
                var error = new ApiMessageError() { message = "Model is invalid" };
                foreach (var prop in ModelState.Values)
                {
                    if (prop.Errors.Any())
                        error.errors.Add(prop.Errors.First().ErrorMessage);
                }

                // Return the error object as a response with an error code
                return Request.CreateResponse<ApiMessageError>(HttpStatusCode.Conflict, error);             
            }

            foreach (var song in album.Songs)
                song.AlbumId = album.Id;

            var matchedAlbum = Albums.Where(alb => alb.Id == album.Id ||
                                            alb.AlbumName == album.AlbumName)
                                     .SingleOrDefault();
            if (matchedAlbum == null)
                Albums.Add(album);
            else
                matchedAlbum = album;

            // return a string to show that the value got here

            var resp = new HttpResponseMessage();
            var content = new StringContent(album.AlbumName +
                                            " " + album.Entered,Encoding.UTF8,"text/plain");
            resp.Content = content;            

            return resp;
        }

        // PUT /api/<controller>/5
        public HttpResponseMessage PutAlbum(Album album)
        {
            return PostAlbum(album);
        }

        // DELETE /api/<controller>/5
        public HttpResponseMessage DeleteAlbum(string title)
        {
            var matchedAlbum = Albums.Where(alb => alb.AlbumName == title)
                                            .SingleOrDefault();
            if (matchedAlbum == null)
                return new HttpResponseMessage(HttpStatusCode.NotFound);

            Albums.Remove(matchedAlbum);

            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }


        public HttpResponseMessage ResetAlbumData()
        {
            Albums = Album.CreateSampleAlbumData();
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }

    public class ApiMessageError
    {
        public string message { get; set; }
        public bool isCallbackError { get; set; }
        public List<string> errors { get; set; }

        public ApiMessageError(string errorMessage = null)
        {
            isCallbackError = true;
            errors = new List<string>();
            message = errorMessage;
        }

        
    }
}