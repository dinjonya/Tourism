using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DinJonYa.Plugs.Time;
using DinJonYa.RedisExchange;
using Telecom.TourismModels.ApiModels;
using DinJonYa.ApiAuthen.Models;
using DinJonYa.ApiAuthen.Models.Bussiness;

namespace DinJonYa.ApiAuthen.ApiControllers
{
    public class AuthenticationController : ApiController
    {
        AuthenBussiness bussiness = new AuthenBussiness();
        RedisBase redis = new RedisBase(Configs.GetConfig.Redis);

        //验证身份获取新的token
        [HttpGet]
        [Route("Api/Authen/ui-GetNewToken")]
        public ApiModel_Authen ApiAuthenGetNewToken()
        {
            ApiModel_Authen authen = new ApiModel_Authen();
            if (!Request.Headers.Contains("appKey") || !Request.Headers.Contains("AppSecrect") ||
                !Request.Headers.Contains("accessIp"))
            {
                authen.Id = -1;
            }
            string appKey = Request.Headers.GetValues("appKey").FirstOrDefault();
            string appSecrect = Request.Headers.GetValues("AppSecrect").FirstOrDefault();
            string accessIp = Request.Headers.GetValues("accessIp").FirstOrDefault();
            ApiModel_Authen model = bussiness.AddNewTokenByAppKey(appKey, appSecrect, accessIp);
            if (model!=null && model.Id > 0)
            {
                //创建token成功 保存redis 过期时间3天
                DateTime expirTime = UnixTimeHelper.FromUnixTime(Convert.ToInt64(model.TokenExpiration));
                TimeSpan ts = expirTime - DateTime.Now;
                redis.StringSet(appKey, model, ts);
            }
            return model;
        }
    }
}
