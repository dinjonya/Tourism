using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DinJonYa.Plugs.Configs;
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
            config = JsonConfigHelper.GetConfiguration<Config_PublishModel>();
        }
        
    }
}
