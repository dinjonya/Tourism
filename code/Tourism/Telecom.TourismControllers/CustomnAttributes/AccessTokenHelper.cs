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

namespace Telecom.TourismControllers.CustomnAttributes
{
    public class AccessTokenHelper
    {
        public int GetOrCreateToken()
        {
            Cache uiCache = HttpContext.Current.Cache;
            ApiModel_Authen authenResult = null;
            if (uiCache["accessToken"] == null)
            {
                //获取token
                authenResult = ApiHelper.GetWebApi<ApiModel_Authen>(ConfigurationManager.AppSettings["ApiAuthenUri"],
                    "Api/Authen/ui-GetNewToken",
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
                    return authenResult.Id;
                }
                else
                    return authenResult == null ? -1 : authenResult.Id;
            }
            return 1;
        }
    }
}
