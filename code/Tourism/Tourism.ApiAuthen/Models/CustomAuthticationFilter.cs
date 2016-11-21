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
using DinJonYa.Plugs.Configs;
using DinJonYa.Plugs.Strings;
using DinJonYa.RedisExchange;
using Telecom.TourismModels.ApiModels;
using Telecom.TourismModels.PublishModels;

namespace Tourism.ApiAuthen.Models
{
    /// <summary>
    /// 用户登录  身份认证与过滤
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class CustomAuthticationFilter : System.Web.Http.Filters.ActionFilterAttribute
    {
        public bool IsAuthentication { get; set; }
        
        private RedisBase redis = null;
        private HttpResponseMessage response = null;
        private ValidateMessage_Config validateMsg = null;
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            List<NoAuthentication> actionFilter = actionContext.ActionDescriptor.GetCustomAttributes<NoAuthentication>().ToList();
            List<NoAuthentication> controllerFilter = actionContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes<NoAuthentication>().ToList();
            //有 NoAuthentication 则 不验证
            if (actionFilter.Count > 1 || controllerFilter.Count > 1)
                return;

            //判断是否拥有token  如果有则继续  没有则回跳
            HttpRequestHeaders requestHeaders = actionContext.Request.Headers;
            #region 验证token

            #region 参数不正确
            if (!requestHeaders.Contains("accessToken") || !requestHeaders.Contains("appKey") || !requestHeaders.Contains("accessIp"))
            {
                validateMsg = SysWebApi.GetValidateByHttpStatusCode(2);
                response = actionContext.Request.CreateResponse((HttpStatusCode)validateMsg.HttpStatusCode,
                    new { Status = validateMsg.Status, Message = validateMsg.Message }, "application/json");
                actionContext.Response = response;
                return;
            }
            #endregion

            #region 判断是否符合身份认证
            bool IsAuthentication = false;
            bool hashToken = false;
            string appKey = requestHeaders.GetValues("appKey").FirstOrDefault();
            string accessToken = requestHeaders.GetValues("accessToken").FirstOrDefault();
            string accessIp = requestHeaders.GetValues("accessIp").FirstOrDefault();
            ApiModel_Authen authenModel = null;

            #region 获取appKey 对应存储的 storageToken   如果可以成功获取 则判断 token是否匹配
            redis = SysWebApi.Redis;
            //如果redis没有 则读取数据库 存入redis
            if (redis.StringHasKey(appKey))
            {
                hashToken = true;
                authenModel = redis.StringGet<ApiModel_Authen>(appKey);
                if (accessIp == "127.0.0.1" || accessIp == "localhost" || accessIp == "::1" || accessIp == "." ||
                    authenModel.AllowIps.Contains(accessIp))
                {
                    //ip 正确 判断token是否正确
                    IsAuthentication = authenModel.Token == accessToken;
                }
                else
                {
                    //ip不正确
                    IsAuthentication = false;
                    validateMsg = SysWebApi.GetValidateByHttpStatusCode(4);
                    response = actionContext.Request.CreateResponse((HttpStatusCode) validateMsg.HttpStatusCode,
                        new {Status = validateMsg.Status, Message = validateMsg.Message}, "application/json");
                    actionContext.Response = response;
                    return;
                }
            }
            #endregion
            //从redis中获取token 并且判断
            if (hashToken)
            {
                #region redis有token  验证token
                //验证通过
                if (IsAuthentication)
                {
                    //validateMsg = SysWebApi.GetValidateByHttpStatusCode(0);
                    //response = actionContext.Request.CreateResponse((HttpStatusCode) validateMsg.HttpStatusCode,
                    //    new {Status = validateMsg.Status, Message = validateMsg.Message}, "application/json");
                    return;
                }
                else
                {
                    //有token 但是验证失败
                    validateMsg = SysWebApi.GetValidateByHttpStatusCode(1);
                    response = actionContext.Request.CreateResponse((HttpStatusCode)validateMsg.HttpStatusCode,
                        new { Status = validateMsg.Status, Message = validateMsg.Message }, "application/json");
                    actionContext.Response = response;
                    return;
                }
                #endregion
            }
            else
            {
                #region redis没有token 要求用户重新验证
                //没有token 需要重新验证
                validateMsg = SysWebApi.GetValidateByHttpStatusCode(3);
                response = actionContext.Request.CreateResponse((HttpStatusCode)validateMsg.HttpStatusCode,
                    new { Status = validateMsg.Status, Message = validateMsg.Message }, "application/json");
                actionContext.Response = response;
                return;
                #endregion
            }

            #endregion

            #endregion
        }

    }
}
