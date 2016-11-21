using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telecom.TourismModels.MQModels
{
    public class MQLog_Model : IMQ_Model
    {
        public string Message { get; set; }

        public Exception Exc { get; set; }

        public Loglevel LogLevel { get; set; }

        public string LogLocation { get; set; }

        public string ipLocation;

        public string IpLocation
        {
            get { return ipLocation; }
            set { ipLocation = value; }
        }

        private string routingKey;

        public string RoutingKey
        {
            get { return routingKey; }
            set { routingKey = value; }
        }
    }
    public enum Loglevel
    {
        Debug,
        Warn,
        Info,
        Fatal,
        Error
    }
}
