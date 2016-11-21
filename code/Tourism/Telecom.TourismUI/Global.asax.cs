using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using DinJonYa.Plugs.Configs;
using Telecom.TourismControllers;
using Telecom.TourismControllers.CustomnAttributes;
using Telecom.TourismModels.PublishModels;

namespace Telecom.TourismUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalRegistrAreas.RegisterArea();
            GlobalRegistrRouteConfig.RegisterRoutes(RouteTable.Routes);
            GlobalRegistrFilters.RegisterGlobalFilters(GlobalFilters.Filters);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            CustomExceptionHandler.OnException(((MvcApplication) sender).Context);
        }
    }
}
