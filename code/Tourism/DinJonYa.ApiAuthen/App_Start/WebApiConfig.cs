﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using DinJonYa.ApiAuthen.CustomnAttributes;

namespace DinJonYa.ApiAuthen
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务
            config.Filters.Add(new CustomExceptionFilterAttribute());
            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
