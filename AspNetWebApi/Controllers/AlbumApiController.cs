using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using MusicAlbums;
using System.Net.Http.Headers;

namespace AspNetWebApi.Controllers.Controllers
{
    public class AlbumApiController : ApiController
    {
        // sample data - static list
        static List<Album> Albums = Album.CreateSampleAlbumData();

        
        public IQueryable<Album> Get()
        {
            return Albums.OrderBy(alb => alb.Artist).AsQueryable();
        }

       
        public Album Get(string id)
        {
            var album = Albums.Where(alb => alb.AlbumName.Contains(id)).SingleOrDefault();
            return album;
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
            foreach (var song in album.Songs)
            {
                song.AlbumId = album.Id;
            }
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
}