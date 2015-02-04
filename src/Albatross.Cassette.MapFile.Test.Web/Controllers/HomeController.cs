using System;
using System.Linq;
using System.Web.Mvc;

namespace Albatross.Cassette.MapFile.Test.Web.Controllers
{
    public class HomeController : Controller
    {
        [Route]
        public ActionResult Index()
        {
            return this.View();
        }
    }
}