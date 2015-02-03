using System;
using System.Linq;
using System.Web.Mvc;

namespace Albatross.Cassette.TypeScript.Test.Web.Controllers
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