using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DinJonYa.Plugs.Configs;
using DinJonYa.RedisExchange;
using Telecom.TourismModels.PublishModels;
using DinJonYa.ApiAuthen.Models;

namespace DinJonYa.ApiAuthen.ApiControllers
{
    public class SysConfigController : ApiController
    {
        RedisBase redis = new RedisBase(Configs.GetConfig.Redis);
        [HttpGet]
        [Route("Sys/Config/GetConfig")]
        public Config_PublishModel GetPublishConfig()
        {
            if (redis.StringHasKey("PublishConfig"))
            {
                return redis.StringGet<Config_PublishModel>("PublishConfig");
            }
            Config_PublishModel config = Configs.GetConfig;
            redis.StringSet("PublishConfig", config);
            return config;
        }
    }
}
