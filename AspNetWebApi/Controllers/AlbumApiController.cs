using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using System.Web.Http;

using MusicAlbums;
using System.Net;
using System.Text;

namespace AspNetWebApi.Controllers
{
    public class AlbumApiController : ApiController
    {

        public IEnumerable<Album> GetAlbums()
        {
            var albums = AlbumData.Current.OrderBy(alb => alb.Artist);
            return albums;
        }

        /// alternate implementation returning HttpResponseMessage
        //public HttpResponseMessage GetAlbums()
        //{
        //    var albums = AlbumData.Current.OrderBy(alb => alb.Artist);

        //    var resp = new HttpResponseMessage(HttpStatusCode.OK);
        //    resp.Content = new ObjectContent<IEnumerable<Album>>(
        //                         albums, new JsonMediaTypeFormatter());

        //    //var resp = Request.CreateResponse<IEnumerable<Album>>(HttpStatusCode.OK, albums);

        //    resp.Headers.ConnectionClose = true;
        //    resp.Headers.CacheControl = new CacheControlHeaderValue();
        //    resp.Headers.CacheControl.Public = true;

        //    return resp;
        //}

        public Album GetAlbum(string title)
        {
            var album = AlbumData.Current
                            .SingleOrDefault(alb => alb.AlbumName.Contains(title));
            return album;
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

            var matchedAlbum = AlbumData.Current
                            .SingleOrDefault(alb => alb.Id == album.Id ||
                                             alb.AlbumName == album.AlbumName);
            if (matchedAlbum == null)
                AlbumData.Current.Add(album);
            else
                matchedAlbum = album;

            // return a string to show that the value got here
            var resp = Request.CreateResponse(HttpStatusCode.OK);
            resp.Content = new StringContent(album.AlbumName + " " + album.Entered.ToString(),
                                                Encoding.UTF8, "text/plain");
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
            var matchedAlbum = AlbumData.Current.Where(alb => alb.AlbumName == title)
                                            .SingleOrDefault();
            if (matchedAlbum == null)
                return new HttpResponseMessage(HttpStatusCode.NotFound);

            AlbumData.Current.Remove(matchedAlbum);

            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }
    }
}