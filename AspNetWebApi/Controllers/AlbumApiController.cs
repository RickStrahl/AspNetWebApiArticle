using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using MusicAlbums;
using System.Net.Http.Headers;
using System.Net;

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

        public Album GetAlbum(string title)
        {            
            var album = Albums.Where(alb => alb.AlbumName.Contains(title)).SingleOrDefault();            
            return album;
        }

        public HttpResponseMessage GetAlbumArt(string title)
        {
            var album = GetAlbum(title);
            if (album == null)
                return new HttpResponseMessage<ApiMessageError>( 
                        new ApiMessageError { message = "Album not found"}, 
                                              HttpStatusCode.NotFound);

            // kinda silly - we would normally serve this directly                     
            var http = new WebClient();
            var imageData = http.DownloadData(album.AlbumImageUrl);
    
            var result = new HttpResponseMessage();            
            result.Content = new ByteArrayContent(imageData);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");            
            
            return result;
        }
        

        public Westwind.StockServer.StockQuote GetAlbum2(string id)
        {
            return new Westwind.StockServer.StockQuote();
        }

        // POST /api/<controller>
        public HttpResponseMessage Post(Album album)
        {
            if (!this.ModelState.IsValid)
            {
                var resp = new HttpResponseMessage<object>(new { message =  "Invalid Model data",isError = true },
                                                       new MediaTypeHeaderValue("application/json"));
                resp.StatusCode = System.Net.HttpStatusCode.Conflict;
                return resp;
                
            }

            // fix up song.AlbumIds
            //foreach (var song in album.Songs)
            //{
            //    song.AlbumId = album.Id;
            //}
            Albums.Add(album);

            // return a string to show that the value got here
            return new HttpResponseMessage<string>(album.AlbumName + " " + album.Entered);
        }

        // PUT /api/<controller>/5
        public void Put(int id, string value)
        {
        }

        // DELETE /api/<controller>/5
        public void Delete(int id)
        {
        }
    }

    public class ApiMessageError
    {
        public string message { get; set; }
        public bool isCallbackError { get; set; }
    }
}