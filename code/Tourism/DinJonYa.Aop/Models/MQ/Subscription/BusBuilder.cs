using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using EasyNetQ;
using Telecom.TourismModels.PublishModels;

namespace DinJonYa.Aop.Models.MQ.Subscription
{

    /// <summary>
    /// 消息服务器连接器
    /// </summary>
    public class BusBuilder
    {
        public static IBus CreateMessageBus()
        {
            // 消息服务器连接字符串
            Aop_Config config = SysWebApi.Configs.Aop;
            string connString = "host={0};virtualHost={1};username={2};password={3}";
            return RabbitHutch.CreateBus(string.Format(connString,
                config.RabbitMQ.Host,
                config.RabbitMQ.VirtualHost,
                config.RabbitMQ.UserName,
                config.RabbitMQ.Password));
        }


    }
}
