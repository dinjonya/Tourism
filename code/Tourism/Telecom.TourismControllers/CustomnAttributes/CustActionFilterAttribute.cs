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

            ApiModel_Authen authenResult = null;
            Cache uiCache = HttpContext.Current.Cache;
            if (uiCache["accessToken"] == null)
            {
                //获取token
                authenResult = ApiHelper.GetWebApi<ApiModel_Authen>(Configs.GetConfig.ApiAuthen.ApiAuthenUri, "Api/Authen/ui-GetNewToken",
                new Dictionary<string, string>
                {
                    {"appKey", ConfigurationManager.AppSettings["appKey"]},
                    {"AppSecrect", ConfigurationManager.AppSettings["AppSecrect"]},
                    {"accessIp", HttpUtil.GetClientIP()}
                });
                if (authenResult != null && authenResult.Id > 0)
                {
                    DateTime dt = DateTime.Now;
                    DateTime tokenExpirTime = UnixTimeHelper.FromUnixTime(Convert.ToInt64(authenResult.TokenExpiration));
                    uiCache.Insert("accessToken", authenResult.Token, null,
                        dt.AddSeconds((tokenExpirTime - dt).TotalSeconds), TimeSpan.Zero);
                    base.OnActionExecuting(filterContext);
                    return;
                }
                if (authenResult == null)
                {
                    filterContext.Result = new RedirectResult("/ErrorPage/Error500/-1", true);
                    return;
                }
                filterContext.Result = new RedirectResult("/ErrorPage/Error500/" + authenResult.Id, true);
                return;
            }
        }
    }
}
