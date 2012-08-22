using System;
using System.Web.Routing;
using System.Web.Http;
using System.Globalization;
using Westwind.Web.WebApi;
//using Westwind.Web.WebApi;

namespace AspNetWebApi
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {           
            

            // Display errors in response locally
            GlobalConfiguration
                   .Configuration
                   .IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Never;

            RegisterApiRoutes(GlobalConfiguration.Configuration);

            // WebApi Configuration to hook up formatters and message handlers
            // optional
            RegisterApis(GlobalConfiguration.Configuration);
        }

        private void RegisterApiRoutes(HttpConfiguration configuration)
        {
            RouteTable.Routes.MapHttpRoute(
                name: "AlbumRpcApiAction",
                routeTemplate: "albums/rpc/{action}/{title}",
                defaults: new
                {
                    title = RouteParameter.Optional,
                    controller = "AlbumRpcApi",
                    action = "GetAblums"
                }
            );

            RouteTable.Routes.MapHttpRoute(
                name: "SamplesApiAction",
                routeTemplate: "samples/{action}/{title}",
                defaults: new
                {
                    title = RouteParameter.Optional,
                    controller = "SamplesApi",
                }
            );

            RouteTable.Routes.MapHttpRoute(
                name: "ValueSamplesApiAction",
                routeTemplate: "valueprovider/{action}",
                defaults: new
                {
                    title = RouteParameter.Optional,
                    controller = "ValueProviderApi",
                }
            );


            RouteTable.Routes.MapHttpRoute(
                name: "AlbumApiActionImage",
                routeTemplate: "albums/{title}/image",
                defaults: new
                {
                    title = RouteParameter.Optional,
                    controller = "AlbumRpcApi",
                    action = "AlbumArt"
                }
            );


            // Verb Routing 
            RouteTable.Routes.MapHttpRoute(
                    name: "AlbumsVerbs",
                    routeTemplate: "albums/{title}",
                    defaults: new
                    {
                        title = RouteParameter.Optional,
                        controller = "AlbumApi"
                    }
                );
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

            config.Formatters.Insert(0, new JsonpFormatter());           

            // Add an exception filter
            //GlobalConfiguration.Configuration.Filters.Add(new UnhandledExceptionFilter());
            //config.Filters.Add(new UnhandledExceptionFilter());

        }
        
    }


}

