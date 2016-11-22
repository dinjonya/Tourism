/// <summary> 
///     作者：
///     时间：2016/9/1 16:52:28  
///     部门：  
///     公司：Axon
///     版权：2016-2012  
///  CLR版本：4.0.30319.42000 
///     说明：本代码版权归Axon所有
/// 唯一标识：69212f21-6ae7-4cac-ba38-b4c4ef9ba523 
///
/// 更改作者：
/// 更改说明：
/// </summary>

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Mvc;
using DinJonYa.Aop.Models;
using DinJonYa.Plugs.Configs;
using DinJonYa.Plugs.Strings;
using DinJonYa.RedisExchange;
using Telecom.TourismModels.ApiModels;
using Telecom.TourismModels.PublishModels;

namespace DinJonYa.Aop.CustomnAttributes
{
    /// <summary>
    /// 用户登录  身份认证与过滤
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class CustActionFilterAttribute : System.Web.Http.Filters.ActionFilterAttribute
    {
        public bool IsAuthentication { get; set; }

        private RedisBase redis = SysWebApi.Redis;
        private HttpResponseMessage response = null;
        private ValidateMessage_Config validateMsg = null;
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            //判断是否拥有token  如果有则继续  没有则回跳
            HttpRequestHeaders requestHeaders = actionContext.Request.Headers;
            #region 验证token

            #region 参数不正确
            if (!requestHeaders.Contains("appKey"))
            {
                validateMsg = SysWebApi.GetValidateByHttpStatusCode(2);
                response = actionContext.Request.CreateResponse((HttpStatusCode)validateMsg.HttpStatusCode,
                    new { Status = validateMsg.Status, Message = validateMsg.Message }, "application/json");
                actionContext.Response = response;
                return;
            }
            else
            {
                string appKey = requestHeaders.GetValues("appKey").FirstOrDefault();
                if (redis.StringHasKey(appKey))
                {
                    return;
                }
                else
                {
                    validateMsg = SysWebApi.GetValidateByHttpStatusCode(2);
                    response = actionContext.Request.CreateResponse((HttpStatusCode)validateMsg.HttpStatusCode,
                        new { Status = validateMsg.Status, Message = validateMsg.Message }, "application/json");
                    actionContext.Response = response;
                    return;
                }
            }
            #endregion

            #endregion
        }

    }
}
