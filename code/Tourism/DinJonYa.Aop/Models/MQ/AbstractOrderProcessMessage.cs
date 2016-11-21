using System.Reflection;
using System.Web;
using DinJonYa.Aop.Models.IP;
using DinJonYa.Aop.Models.MQ.OrderProcessMessage;
using DinJonYa.Plugs.Configs;
using Telecom.TourismModels.MQModels;
using Telecom.TourismModels.PublishModels;

namespace DinJonYa.Aop.Models.MQ
{
    public abstract class AbstractOrderProcessMessage : IProcessMessage
    {
        protected static IPScanner ipSearch= new IPScanner { DataPath = HttpContext.Current.Server.MapPath("~/Content/IPData/qqwry.dat") };
        public static void InitOrderProcess(Aop_Config aopConfig)
        {
            if (!aopConfig.RabbitMQ.Enabled)
                return;
            foreach (RouteingKeys_Config routeingKeyCfg in aopConfig.RabbitMQ.RouteingKeys)
            {
                IProcessMessage ipm =
                    Assembly.GetAssembly(typeof (AbstractOrderProcessMessage)).CreateInstance(routeingKeyCfg.Handler) as IProcessMessage;
                MQHelper.Subscribe(routeingKeyCfg.Key, ipm);
            }
        }
    }
}
