using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Telecom.TourismModels.MQModels
{
    public interface IMQ_Model
    {
        string IpLocation { get; set; }

        string RoutingKey { get; set; }

    }

    
}
