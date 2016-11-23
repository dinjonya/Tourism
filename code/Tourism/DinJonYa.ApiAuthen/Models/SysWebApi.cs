using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using DinJonYa.Plugs.Configs;
using DinJonYa.RedisExchange;
using Telecom.TourismModels.PublishModels;

namespace DinJonYa.ApiAuthen.Models
{
    public class SysWebApi
    {
        private static Config_PublishModel config = JsonConfigHelper.GetConfiguration<Config_PublishModel>();

        public static Config_PublishModel Configs
        {
            get { return config; }
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

    }
}