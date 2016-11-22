using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using DinJonYa.Plugs.Configs;
using DinJonYa.RedisExchange;
using Telecom.TourismModels.PublishModels;
using Telecom.TourismWebApi.CustomnAttributes;
using Telecom.TourismWebApi.Models;

namespace Telecom.TourismWebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            CustomExceptionHandler.OnException(((WebApiApplication)sender).Context);
        }
    }

    
}
