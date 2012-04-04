﻿using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using System.Web.Http;

using MusicAlbums;
using System.Net;
using System.Xml;

namespace AspNetWebApi.Controllers
{
    
  

    /// <summary>
    /// This class demonstrates routing using traditional 
    /// action method routing which tends to be way more
    /// flexible than the limited HTTP verb routing.    
    /// </summary>
    public class AlbumRpcApiController : ApiController
    {
        [HttpGet,Queryable]
        public IQueryable<Album> SortableAlbums()
        {
            var albums = AlbumData.Current;
            return albums.AsQueryable();
        }

        public static bool IsDotNet45()
        {
            return Environment.Version.Revision > 15000;
        }


        [HttpGet]
        public HttpResponseMessage AlbumArt(string title)
        {            
            var album = AlbumData.Current
                         .FirstOrDefault(abl => abl.AlbumName.StartsWith(title));
            if (album == null)
            {
                var resp = Request.CreateResponse<ApiMessageError>(
                                        HttpStatusCode.NotFound,
                                        new ApiMessageError("Album not found"));
                return resp;

            }

            // kinda silly - we would normally serve this directly  
            // but hey - it's a demo.       
            var http = new WebClient();
            var imageData = http.DownloadData(album.AlbumImageUrl);

            // create response and return 
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new ByteArrayContent(imageData);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");

            return result;
        }

        [HttpGet]
        public HttpResponseMessage ResetAlbumData()
        {
            AlbumData.Current = AlbumData.CreateSampleAlbumData();
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
   }
}