// <summary> 
//     作者：
//     时间：2016/8/22 15:03:07  
//     部门：  
//     公司：Axon
//     版权：2016-2012  
//  CLR版本：4.0.30319.42000 
//     说明：本代码版权归Axon所有
// 唯一标识：d963fde3-5a43-420b-8aef-884027c2934d 
//
// 更改作者：
// 更改说明：
// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Telecom.TourismControllers.Models.ExcHandler;
using Telecom.TourismModels.MQModels;

namespace Telecom.TourismControllers.CustomnAttributes
{

    public class CustomExceptionHandler
    {
        public static void OnException(HttpContext context)
        {
            HttpServerUtility Server = context.Server;
            var ex = Server.GetLastError();
            //记录日志信息  
            var httpStatusCode = (ex is HttpException)?(ex as HttpException).GetHttpCode() : 500; //这里仅仅区分两种错误  
            context.ClearError();
            context.Response.Clear();
            context.Response.StatusCode = httpStatusCode;
            var shouldHandleException = true;
            string controllerName = "ErrorPage";
            string actionName = "";

            switch (httpStatusCode)
            {
                case 404:
                    actionName = "PageNotFound";
                    break;
                default:
                    actionName = "Error";
                    break;
            }

            context.Response.RedirectPermanent("~/" + controllerName + "/" + actionName + "/p=" +
                                               Server.UrlEncode(
                                                   Convert.ToBase64String(
                                                       Encoding.UTF8.GetBytes(context.Request.Url.ToString()))));
        }
    }
}
