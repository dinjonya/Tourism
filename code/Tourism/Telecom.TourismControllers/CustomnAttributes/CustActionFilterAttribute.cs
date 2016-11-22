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
using DinJonYa.Plugs.Time;
using DinJonYa.Plugs.WebApi;
using Telecom.TourismControllers.Models;
using Telecom.TourismModels.ApiModels;

namespace Telecom.TourismControllers.CustomnAttributes
{
    public partial class CustActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Object[] actionFilter = filterContext.ActionDescriptor.GetCustomAttributes(
                typeof(NoActionFilterAttribute), false);
            Object[] controllerFilter = filterContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes(
                typeof(NoActionFilterAttribute), false);
            //有 NoAuthentication 则 不验证
            if (actionFilter.Length > 0 || controllerFilter.Length > 0)
            {
                base.OnActionExecuting(filterContext);
                return;
            }

            int flag = new AccessTokenHelper().GetOrCreateToken();
            if (flag > 0)
            {
                base.OnActionExecuting(filterContext);
                return;
            }
            else
            {
                filterContext.HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                filterContext.Result = new RedirectResult("/ErrorPage/Error500/"+ flag, true);
            }
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            //写入 操作日志
            base.OnResultExecuted(filterContext);
        }
    }
}
