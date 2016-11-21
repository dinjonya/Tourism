using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DinJonYa.Aop.Models.MQ;
using Telecom.TourismModels.MQModels;

namespace DinJonYa.Aop.ApiControllers
{
    public class MQController : ApiController
    {
        // GET api/values
        [Route("Aop/MQ-Log")]
        public void MQLogHandler(MQLog_Model model)
        {
            MQHelper.Publish(model);
        }
    }
}
