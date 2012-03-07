using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

using System.Web.Routing;
using System.Web.Http;

namespace AspNetWebApi
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

          // 
          RouteTable.Routes.MapHttpRoute(
              name: "StockApi",
              routeTemplate: "api/{controller}/{id}",
              defaults: new { id = RouteParameter.Optional }
          );
          RouteTable.Routes.MapHttpRoute(
              name: "StockApi",
              routeTemplate: "api/{controller}/{action}/{id}",
              defaults: new { id = RouteParameter.Optional }
          );
        }

   }
}