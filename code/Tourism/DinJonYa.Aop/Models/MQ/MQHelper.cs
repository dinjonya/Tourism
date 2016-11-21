using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DinJonYa.Aop.Models.MQ;
using DinJonYa.Aop.Models.MQ.OrderProcessMessage;
using DinJonYa.Aop.Models.MQ.Subscription;
using EasyNetQ;
using Newtonsoft.Json;
using Telecom.TourismModels.MQModels;
using Telecom.TourismModels.PublishModels;

namespace DinJonYa.Aop.Models.MQ
{
    public class MQHelper
    {
        /// <summary>
        /// 发送消息
        /// </summary>
        public static void Publish(IMQ_Model model)
        {
            // 创建消息bus
            using (IBus bus = BusBuilder.CreateMessageBus())
            {
                try
                {
                    bus.Publish(model, x => x.WithTopic(model.RoutingKey));
                }
                catch (EasyNetQException ex)
                {
                    //处理连接消息服务器异常 
                }
            }
        }


        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="msg"></param>
        public static void Subscribe(string routeingKey, IProcessMessage ipro)
        {
            // 创建消息bus
            IBus bus = BusBuilder.CreateMessageBus();
            try
            {
                bus.Subscribe<IMQ_Model>(routeingKey, message => ipro.ProcessMessage(message),
                    x => x.WithTopic(routeingKey));
            }
            catch (EasyNetQException ex)
            {
                //处理连接消息服务器异常 
            }
        }
    }
}
