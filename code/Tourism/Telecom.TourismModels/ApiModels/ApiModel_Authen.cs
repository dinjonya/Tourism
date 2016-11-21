using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telecom.TourismModels.ApiModels
{
    public partial class ApiModel_Authen
    {
        public int Id { get; set; }
        public string AppKey { get; set; }
        public string AppName { get; set; }
        public string AppSecrect { get; set; }
        public string Token { get; set; }
        public string TokenExpiration { get; set; }
        public string Salt { get; set; }
        public List<string> AllowIps { get; set; }
    }

    
}
