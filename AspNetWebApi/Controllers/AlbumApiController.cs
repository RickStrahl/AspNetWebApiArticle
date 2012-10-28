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

        //    alternate more verbose implementation returning HttpResponseMessage
        //public HttpResponseMessage GetAlbums()
        //{
        //    var albums = AlbumData.Current.OrderBy(alb => alb.Artist);

        //    // Create a new HttpResponse with Json Formatter explicitly
        //    var resp = new HttpResponseMessage(HttpStatusCode.OK);
        //    resp.Content = new ObjectContent<IEnumerable<Album>>(
        //                            albums, new JsonMediaTypeFormatter());

        //    // Easier: Get Default Formatter based on Content Negotiation
        //    //var resp = Request.CreateResponse<IEnumerable<Album>>(HttpStatusCode.OK, albums);

        //    // Add options that require HttpResponseMessage
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
                // Default Error Result
                //return Request.CreateErrorResponse(HttpStatusCode.Conflict, ModelState);

                //// my custom error class
                //var error = new ApiMessageError() { message = "Model is invalid" };

                //// add errors into our client error model for client
                //foreach(var modelItem in ModelState)
                //{                
                //    var modelError = modelItem.Value.Errors.FirstOrDefault();
                //    if (!string.IsNullOrEmpty(modelError.ErrorMessage))    
                //        error.errors.Add(modelItem.Key + ": " + modelError.ErrorMessage);
                //    else
                //        error.errors.Add(modelItem.Key + ": " + modelError.Exception.Message);
                //}

                // Customized error handling
                var error = new ApiMessageError(ModelState);
                return Request.CreateResponse<ApiMessageError>(HttpStatusCode.Conflict, error);
            }

            // update song id which isn't provided
            foreach (var song in album.Songs)
                song.AlbumId = album.Id;

            // see if album exists already
            var matchedAlbum = AlbumData.Current
                            .SingleOrDefault(alb => alb.Id == album.Id ||
                                             alb.AlbumName == album.AlbumName);
            if (matchedAlbum == null)
                AlbumData.Current.Add(album);
            else
                matchedAlbum = album;

            // return a string to show that the value got here
            var resp = Request.CreateResponse(HttpStatusCode.OK, string.Empty);
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

