using DinJonYa.Plugs.WebApi;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using DinJonYa.Plugs.Http;
using Newtonsoft.Json.Linq;
using Telecom.TourismControllers.Models;
using Telecom.TourismControllers.Models.ExcHandler;
using Telecom.TourismModels.MQModels;
using Telecom.TourismModels.PublishModels;
using Telecom.TourismModels.ValidateModels;

namespace Telecom.TourismControllers.Controllers
{
    public partial  class HomeController : Controller
    {
        private ValidateModel_ValidateResult vResult;
        public ActionResult Index()
        {
            
            Object result = ApiHelper.GetWebApi<Object>(Configs.GetConfig.Base.WebApiUri, "api/test",
                new Dictionary<string, string>
                {
                    {"appKey", ConfigurationManager.AppSettings["appKey"]},
                    {"accessToken", HttpContext.Cache["accessToken"].ToString()},
                    {"accessIp", HttpUtil.GetClientIP()}
                });
            if (result.GetType() == typeof(JObject))
            {
                if ((result as JObject).Property("Status") != null && (result as JObject)["Status"].ToString() == ValidateResultType.Refresh_token.ToString())
                {
                    HttpContext.Cache.Remove("accessToken");
                    return Index();
                }
            }
            ViewBag.TestValue = result.ToString();
            return View();
        }
    }
}
