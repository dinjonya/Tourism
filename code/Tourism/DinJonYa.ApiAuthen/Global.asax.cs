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
using DinJonYa.ApiAuthen.CustomnAttributes;
using DinJonYa.ApiAuthen.Models;
using DinJonYa.ApiAuthen.Models.DBModels;

namespace DinJonYa.ApiAuthen
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

            new RedisBase(Configs.GetConfig.Redis).Cleardb();


            if (Convert.ToBoolean(ConfigurationManager.AppSettings["AutomaticMigration"]))
            {
                Database.SetInitializer<TourismApiAuthenEntities>(new SampleData());
                ContextHelper.Init();
            }
            
        }


        protected void Application_Error(object sender, EventArgs e)
        {
            CustomExceptionHandler.OnException(((WebApiApplication)sender).Context);
        }
    }
}
