/*****************************************
    模块名: 
       作者:                                       
 编写时间: 2014/12/13 16:39:57       

修改作者:   
修改时间:
 
      说明:
*****************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data.Entity.Validation;
using DinJonYa.RedisExchange;

namespace DinJonYa.ApiAuthen.Models.DBModels
{
    public class ContextHelper
    {
        public static void Init()
        {
            using (var db = new TourismApiAuthenEntities())
            {
                var apps = (from apiUser in db.ApiUsers select new
                {
                    Id=apiUser.Id,
                    Appkey=apiUser.AppKey,
                    AppName=apiUser.AppName,
                    Token="",
                    CreateTokenTime= "",
                    InnerApp=apiUser.InnerApp
                }).ToList();
                RedisBase redis = new RedisBase(Configs.GetConfig.Redis);
                foreach (var app in apps)
                {
                    redis.StringSet(app.Appkey, app);
                }
            }
        }
    }
}
