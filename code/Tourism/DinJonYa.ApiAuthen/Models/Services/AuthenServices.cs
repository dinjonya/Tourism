using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DinJonYa.Plugs.Strings;
using DinJonYa.Plugs.Time;
using Telecom.TourismModels.ApiModels;
using DinJonYa.ApiAuthen.Models.DBModels;

namespace DinJonYa.ApiAuthen.Models.Services
{
    public partial class AuthenServices
    {
        public ApiModel_Authen CreateNewTokenByAppKey(string appKey, string appSecrect, string accessIp)
        {
            using (TourismApiAuthenEntities db = new TourismApiAuthenEntities())
            {
                DbModel_ApiUsers model = db.ApiUsers.SingleOrDefault(au => au.AppKey == appKey);
                if (model != null)
                {
                    ApiModel_Authen authen = new ApiModel_Authen();
                    string appName = model.AppName;
                    string salt = model.Salt;
                    string secrect = StringMD5.StringToMd5ToLower(appSecrect + salt + appName);
                    string dbSecrect = model.AppSecrect;    //fc611f2be8590f0252fd9e3f7e89e565
                    if (dbSecrect != secrect)
                    {
                        authen.Id = -2;     //密码不正确
                        return authen;
                    }
                    List<string> ipList =
                        (from ip in db.ApiUserIps where ip.AppKey == appKey select ip.AllowIp).ToList();
                    if (accessIp != "127.0.0.1" && accessIp == "localhost" && accessIp == "::1" && accessIp == "." &&
                        ipList.Contains(accessIp))
                    {
                        authen.Id = -1;     //ip不允许
                        return authen;
                    }

                    authen.Id = model.Id;
                    authen.AppKey = model.AppKey;
                    authen.AppName = model.AppName;

                    string dbToken = model.Token;
                    DateTime tokenTime = string.IsNullOrEmpty(model.CreateTokenTime)
                        ? DateTime.MinValue
                        : UnixTimeHelper.FromUnixTime(Convert.ToInt64(model.CreateTokenTime));
                    DateTime newdt = DateTime.Now;
                    DateTime expirTime = DateTime.MinValue;
                    //dbToken不为null   并且时间<3天以内   token任然有效
                    if (!string.IsNullOrEmpty(dbToken) && (newdt - tokenTime).TotalSeconds < 3*24*60*60)
                    {
                        authen.Token = dbToken;
                        DateTime createDt = UnixTimeHelper.FromUnixTime(Convert.ToInt64(model.CreateTokenTime));
                        expirTime = createDt.AddDays(3);
                        authen.TokenExpiration = UnixTimeHelper.FromDateTime(expirTime).ToString();
                        return authen;
                    }


                    //需要创建 新的 Token
                    string token = Guid.NewGuid().ToString("N");
                    authen.Token = token;
                    model.Token = authen.Token;
                    DateTime dt = DateTime.Now;
                    model.CreateTokenTime = UnixTimeHelper.FromDateTime(dt).ToString();
                    expirTime = dt.AddDays(3);
                    authen.TokenExpiration = UnixTimeHelper.FromDateTime(expirTime).ToString(); //3天后过期
                    db.SaveChanges();   //生成新的token并保存数据库
                    return authen;
                }
                return null;
            }
        }
    }
}