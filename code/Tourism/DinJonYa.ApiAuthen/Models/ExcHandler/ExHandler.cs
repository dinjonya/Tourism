using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DinJonYa.Plugs.Http;
using DinJonYa.Plugs.WebApi;
using Telecom.TourismModels.MQModels;
using DinJonYa.ApiAuthen.Models;

namespace DinJonYa.ApiAuthen.Models.ExcHandler
{
    public class ExHandler
    {
        private static readonly string AopUri = SysWebApi.Configs.Aop.AopUri;

        /// <summary>
        /// 错误处理  写入日志
        /// </summary>
        /// <param name="ex">错误对象</param>
        /// <param name="loglevel">错误级别 默认为error</param>
        /// <param name="routeKey">队列路由log.* 默认为log.error</param>
        public static void TreatedWriteLog(Exception ex, string message = "",
            Loglevel loglevel = Loglevel.Error, string routeKey = "log.error", string ip = "127.0.0.1")
        {
            {
                ApiHelper.PostWebApi<int>(AopUri, "Aop/MQ-Log",
                    new MQLog_Model
                    {
                        LogLevel = loglevel,
                        Exc = ex,
                        IpLocation = ip,
                        RoutingKey = routeKey,
                        Message = message,
                    }, new Dictionary<string, string>
                    {
                        {"appKey", ConfigurationManager.AppSettings["appKey"]}
                    });
            }
        }
    }
}
