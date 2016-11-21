﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DinJonYa.Plugs.Configs;
using DinJonYa.Plugs.Http;
using DinJonYa.Plugs.WebApi;
using Telecom.TourismModels.ApiModels;
using Telecom.TourismModels.PublishModels;

namespace Telecom.TourismControllers.Models
{
    public class Configs
    {
        public static Config_PublishModel GetConfig
        {
            get { return config; }
        }
        private static Config_PublishModel config = null;
        private static Resources_Config resourcesConfig = null;

        public static Resources_Config ResourcesConfig
        {
            get { return config.Resources; }
        }

        static Configs()
        {
            config = ApiHelper.GetWebApi<Config_PublishModel>(ConfigurationManager.AppSettings["ApiAuthenUri"], "Sys/Config/GetConfig");
        }
    }
}
