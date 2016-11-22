using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using DinJonYa.Plugs.Configs;
using DinJonYa.Plugs.Http;
using DinJonYa.Plugs.WebApi;
using DinJonYa.RedisExchange;
using Telecom.TourismModels.ApiModels;
using Telecom.TourismModels.PublishModels;

namespace Telecom.TourismWebApi.Models
{
    public class SysWebApi
    {
        private static Config_PublishModel config = null;

        public static Config_PublishModel Configs
        {
            get { return config; }
        }
        private static RedisBase redis = null;
        static SysWebApi()
        {
            config = ApiHelper.GetWebApi<Config_PublishModel>(ConfigurationManager.AppSettings["ApiAuthenUri"],
                "Sys/Config/GetConfig");
            redis = new RedisBase(config.Redis);
        }

        
        public static RedisBase Redis
        {
            get { return redis; }
        }

        public static ValidateMessage_Config GetValidateByHttpStatusCode(int statusCode)
        {
            for (int i = 0; i < config.ApiAuthen.ValidateMessages.Count; i++)
            {
                ValidateMessage_Config msg = config.ApiAuthen.ValidateMessages[i];
                if (msg.StatusCode == statusCode)
                {
                    return msg;
                }
            }
            return null;
        }



    }
}