using System;
using System.Web.Routing;
using System.Web.Http;
using System.Linq;
using System.Data.Entity;
using System.Data;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using MusicAlbums;
using Westwind.Web.WebApi;

namespace AspNetWebApi
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {       

            //// Http Verb based routing (use for resources whenever possible)
            //RouteTable.Routes.MapHttpRoute(
            //    name: "StockApiVerbs",
            //    routeTemplate: "stocks/{symbol}",
            //    defaults: new { symbol = RouteParameter.Optional,
            //                    controller = "StockApi" }
            //);

            // Action based routing (used for RPC calls)
            //RouteTable.Routes.MapHttpRoute(
            //    name: "StockApi",
            //    routeTemplate: "stocks/{action}/{symbol}",
            //    defaults: new
            //    {
            //        symbol = RouteParameter.Optional,
            //        controller = "StockApi"
            //    }
            //);



RouteTable.Routes.MapHttpRoute(
    name: "AlbumApiActionImage",
    routeTemplate: "albums/{title}/image",
    defaults: new
    {
        title = RouteParameter.Optional,
        controller = "AlbumApi",
        action = "GetAlbumArt"
    }
);
           RouteTable.Routes.MapHttpRoute(
                name: "AlbumApiAction",
                routeTemplate: "albums/{title}",
                defaults: new
                {
                    title = RouteParameter.Optional,
                    controller = "AlbumApi",
                    action = "GetAlbums"
                }
            );


            RouteTable.Routes.MapHttpRoute(
                name: "StockProfileApi",
                routeTemplate: "stocks/{action}/{symbol}",
                defaults: new
                {
                    symbol = RouteParameter.Optional,
                    controller = "StockApi"
                }
            );

          

            // create/update database on changes
            //Database.SetInitializer<StockContext>(new StockContextInitializer());
            //Database.SetInitializer<AlbumContext>(new AlbumContextDbInitializer());

            // WebApi Configuration to hook up formatters and message handlers
            // optional
            RegisterApis(GlobalConfiguration.Configuration);

            var formatter = GlobalConfiguration.Configuration.Formatters;
            
        }

        public static void RegisterApis(HttpConfiguration config)
        {
            var formatters = config.Formatters;
#if false
            // remove default Xml handler
            var matches = config.Formatters
                                .Where(f => f.SupportedMediaTypes
                                             .Where(m => m.MediaType.ToString() == "application/xml" ||
                                                         m.MediaType.ToString() == "text/xml")
                                             .Count() > 0)
                                .ToList() ;
            foreach (var match in matches)
                config.Formatters.Remove(match);    
#endif

            // Add JavaScriptSerializer  formatter instead - add at top to make default
            //config.Formatters.Insert(0, new JavaScriptSerializerFormatter());

            // Add Json.net formatter - add at the top so it fires first!
            // This leaves the old one in place so JsonValue/JsonObject/JsonArray still are handled
            //config.Formatters.Insert(0, new JsonNetFormatter());
            //var formatter = config.Formatters[0];
            //formatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));                        
           
        }
    }
}