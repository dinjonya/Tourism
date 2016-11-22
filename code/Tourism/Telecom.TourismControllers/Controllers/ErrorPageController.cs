using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Telecom.TourismControllers.CustomnAttributes;

namespace Telecom.TourismControllers.Controllers
{
    [NoActionFilter]
    public partial class ErrorPageController : Controller
    {
        public ActionResult Error500(string statue)
        {
            HttpContext.Response.Headers.Add("Cache-Control", "no-store");
            return View();
        }

        public ActionResult PageNotFound()
        {
            return View();
        }
    }
}
