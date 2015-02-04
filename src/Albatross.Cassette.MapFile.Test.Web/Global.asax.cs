using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace Albatross.Cassette.MapFile.Test.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteTable.Routes.MapMvcAttributeRoutes();
        }
    }
}