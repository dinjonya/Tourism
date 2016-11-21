//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Web;

//namespace Telecom.TourismWebApi.Models.ExcHandler
//{
//    public class ExHandler
//    {
//        private static string aopUri = ConfigurationManager.AppSettings["AopUri"];

//        /// <summary>
//        /// 错误处理  写入日志
//        /// </summary>
//        /// <param name="ex">错误对象</param>
//        /// <param name="loglevel">错误级别 默认为error</param>
//        /// <param name="routeKey">队列路由log.* 默认为log.error</param>
//        public static void TreatedError(Exception ex, string message = "",
//            Loglevel loglevel = Loglevel.Error, string routeKey = "log.error", string ip = "127.0.0.1")
//        {
//            {
//                ApiHelper.PostWebApi<int>(aopUri, "Aop/MessageHandler",

//                    new MQMessageError_Model
//                    {
//                        LogLevel = loglevel,
//                        Exc = ex,
//                        IpLocation = ip,
//                        RoutingKey = routeKey,
//                        Message = message,
//                    });
//            }
//        }

//        public static void TreatedError(DBModel_AccumulateCardLog log, string routeKey = "mysqllog.info", string ip = "127.0.0.1")
//        {
//            {
//                ApiHelper.PostWebApi<int>(aopUri, "Aop/AcHandler",
//                    new MQACLog_Model
//                    {
//                        IpLocation = ip,
//                        RoutingKey = routeKey,
//                        Log=log
//                    });
//            }
//        }
//    }
//}
