﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using DinJonYa.Aop.CustomnAttributes;
using DinJonYa.Aop.Models;
using DinJonYa.Aop.Models.DBModels;
using DinJonYa.Aop.Models.MQ;
using DinJonYa.Plugs.Configs;
using DinJonYa.Plugs.WebApi;
using Telecom.TourismModels.PublishModels;

namespace DinJonYa.Aop
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
                Database.SetInitializer<DinJonYaAopEntities>(new SampleData());
                //ContextHelper.Init();
            }

            //开启rabbitMQ消息队列
            AbstractOrderProcessMessage.InitOrderProcess();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            CustomExceptionHandler.OnException(((WebApiApplication)sender).Context);
        }
    }
}
