using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using DinJonYa.Aop.CustomnAttributes;

namespace DinJonYa.Aop
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务
            config.Filters.Add(new CustActionFilterAttribute());
            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DinJonYa.Aop_api_default",
                routeTemplate: "DJY-Aop/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
