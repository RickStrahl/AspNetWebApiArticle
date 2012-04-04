﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Net.Http.Formatting;
using MusicAlbums;

namespace AspNetWebApi
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var formVars = new Dictionary<string, string>();
            formVars.Add("message", "Some FormVariable Value");
            //var content = new FormUrlEncodedContent(formVars);
            
            //var content = new StringContent("\"Some Value\"",Encoding.UTF8,"application/json");                       
            //var buffer = content.ReadAsStringAsync().Result;
            //var content = new ObjectContent<string>("Hello World", new JsonMediaTypeFormatter());

            //var content = new ObjectContent<DateTime>(new DateTime(2012, 1, 1), new JsonMediaTypeFormatter(), "application/json");
            //var content = new ObjectContent<string>("Data from the client", new JsonMediaTypeFormatter(), "application/json");
            
            //var content = new ObjectContent<Album>(new Album() { AlbumName = "Dirty Deeds", YearReleased=1976 },
            //                                                     new XmlMediaTypeFormatter() );

            //var content = new Content
var client = new HttpClient();
var album = new Album() { AlbumName = "PowerAge", Artist = "AC/DC" };
var result = client.PostAsync<Album>("http://localhost/AspNetWebApi/albums/rpc/ReturnXmlDocument",
                                        album, new XmlMediaTypeFormatter())
                    .Result;

            //var result = client.PostAsync("http://rasxps/AspNetWebApi/albums/rpc/ReturnFormVariableString", content).Result;
            //var result = client.PostAsync("http://rasxps/AspNetWebApi/albums/rpc/ReturnString", content).Result;
            //var result = client.PostAsync("http://rasxps/AspNetWebApi/albums/rpc/ReturnMessageModel", content).Result;
            //var result = client.PostAsync("http://rasxps/AspNetWebApi/albums/rpc/ReturnRequestBody", content).Result;
            
            //HttpResponseMessage result = null;

            //result = client.PostAsJsonAsync<string>("http://rasxps/AspNetWebApi/albums/rpc/ReturnStringId","Hello World").Result;
            
            //var result = client.PostAsJsonAsync<string>("http://rasxps/AspNetWebApi/albums/rpc/ReturnString", "Hello World").Result;
            //var result = client.PostAsJsonAsync<DateTime>("http://rasxps/AspNetWebApi/albums/rpc/ReturnDateTime", new DateTime(2012, 1, 1)).Result;

            Response.Write("<pre>" + result.Content.ReadAsStringAsync().Result + "</pre>");

            Response.Write(Environment.UserName);
        }
    }
}