using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using DinJonYa.Plugs.Configs;
using DinJonYa.RedisExchange;
using Telecom.TourismModels.PublishModels;
using Tourism.ApiAuthen.Models.DBModels;

namespace Tourism.ApiAuthen
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
            if (Convert.ToBoolean(ConfigurationManager.AppSettings["AutomaticMigration"]))
            {
                Database.SetInitializer<TourismApiAuthenEntities>(new SampleData());
                ContextHelper.Init();
            }
            new RedisBase().Cleardb();
        }
    }
}
