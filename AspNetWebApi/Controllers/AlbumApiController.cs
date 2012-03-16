using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using MusicAlbums;
using System.Net.Http.Headers;
using System.Net;
using System.Configuration;
using System.Text;

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
    catch(Exception ex)
    {
        ThrowSafeException(ex.Message);
        ///throw new InvalidOperationException("Bogus excpetion fired");
    }
}

private void ThrowSafeException(string message,
                                HttpStatusCode statusCode = 
                                 HttpStatusCode.InternalServerError)
{
    var errMsg = new HttpResponseMessage<ApiMessageError>(
            new ApiMessageError() { message = message },
            statusCode);

   throw new HttpResponseException(errMsg);
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
                return new HttpResponseMessage<ApiMessageError>(
                        new ApiMessageError { message = "Album not found" },
                                              HttpStatusCode.NotFound);

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
                var resp = new HttpResponseMessage<ApiMessageError>(
                                           error, HttpStatusCode.Conflict);
                return resp;
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
            return new HttpResponseMessage<string>(album.AlbumName +
                                                    " " + album.Entered);
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

        public ApiMessageError()
        {
            isCallbackError = true;
            errors = new List<string>();
        }
    }
}