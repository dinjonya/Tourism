using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using DinJonYa.Plugs.Http;
using DinJonYa.Plugs.Time;
using DinJonYa.Plugs.WebApi;
using Telecom.TourismModels.ApiModels;

namespace Telecom.TourismControllers.Models
{
    public partial  class AuthenApi
    {
        public static string AuthenticationGetToken()
        {
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
                if (authenResult.Id == -1)
                {
                    throw new Exception("Api/Authentication-GetNewToken 参数不正确");
                }
                if (authenResult.Id == -2)
                {
                    throw new Exception("Api/Authentication-GetNewToken appKey或AppSecrect不正确");
                }
                if (authenResult.Id == 0)
                {
                    throw new Exception("Api/Authentication-GetNewToken 系统错误，请联系管理员");
                }
                if (authenResult.Id > 0)
                {
                    DateTime dt = DateTime.Now;
                    DateTime tokenExpirTime = UnixTimeHelper.FromUnixTime(Convert.ToInt64(authenResult.TokenExpiration));
                    uiCache.Insert("accessToken", authenResult.Token, null,
                        dt.AddSeconds((tokenExpirTime - dt).TotalSeconds), TimeSpan.Zero);
                }
            }
            return uiCache["accessToken"].ToString();
        }
    }
}
