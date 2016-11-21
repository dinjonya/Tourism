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
using Telecom.TourismControllers.Models.ExcHandler;
using Telecom.TourismModels.MQModels;

namespace Telecom.TourismControllers.CustomnAttributes
{

    public class CustomExceptionAttribute : FilterAttribute, IExceptionFilter //HandleErrorAttribute
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled == true && (filterContext.Exception as HttpException).GetHttpCode() != 500)
            {
                return;
            }
            Exception exc = filterContext.Exception;
            if (exc != null)
            {
                HttpException hexc = filterContext.Exception as HttpException;
                int httpCode = new HttpException(null, hexc).GetHttpCode();
                if (httpCode == 404)
                {
                    filterContext.Result = new RedirectResult("~/Errors/Page404", true);
                    return;
                }
                if (httpCode == 500)
                {
                    filterContext.Result = new RedirectResult("~/Errors/Page500", true);
                }
            }
            //写入日志 记录
            filterContext.ExceptionHandled = true;//设置异常已经处理
            HttpContext.Current.Response.Redirect("~/Errors/PageError");
        }

    }
}
