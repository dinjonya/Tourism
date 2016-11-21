using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using DinJonYa.Plugs.Configs;
using DinJonYa.RedisExchange;
using Telecom.TourismModels.PublishModels;

namespace Telecom.TourismWebApi.Models
{
    public class SysWebApi
    {
        static SysWebApi()
        {
            redis = new RedisBase();
        }
        private static RedisBase redis = null;
        public static RedisBase Redis
        {
            get { return redis; }
        }
        public static bool RedisEnabled 
        {
            get { return config.Redis.Enabled; }
        }

        public static List<ValidateMessage_Config> ValidateResult
        {
            get { return config.ApiAuthen.ValidateMessages; }
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

        private static Config_PublishModel config = JsonConfigHelper.GetConfiguration<Config_PublishModel>();
        
    }
}