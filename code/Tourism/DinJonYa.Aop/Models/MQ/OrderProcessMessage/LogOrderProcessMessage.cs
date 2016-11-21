using DinJonYa.Aop.Models.IP;
using DinJonYa.Aop.Models.LogHelper;
using Newtonsoft.Json;
using Telecom.TourismModels.MQModels;

namespace DinJonYa.Aop.Models.MQ.OrderProcessMessage
{
    public class LogOrderProcessMessage : AbstractOrderProcessMessage
    {
        public override void ProcessMessage(IMQ_Model model)
        {
            MQLog_Model logModel = model as MQLog_Model;
            logModel.IpLocation = (logModel.IpLocation == "::1" || logModel.IpLocation == "localhost")
                ? "127.0.0.1"
                : logModel.IpLocation;
            ipSearch.IP = logModel.IpLocation;
            logModel.LogLocation = ipSearch.IPLocation();
            Log4NetHelp.LogWriteFile(logModel.LogLevel, logModel);
        }
    }
}
