using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telecom.TourismModels.MQModels;

namespace DinJonYa.Aop.Models.MQ.OrderProcessMessage
{
    public abstract class IProcessMessage
    {
        public abstract void ProcessMessage(IMQ_Model result);
    }
}
