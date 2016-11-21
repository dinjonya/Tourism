using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telecom.TourismModels.ValidateModels
{
    public partial class ValidateModel_ValidateResult
    {
        public ValidateResultType Status { get; set; }
        public string Message { get; set; }
    }

    public enum ValidateResultType
    {
        Authen_success,
        Authen_fail,
        Paramar_fail,
        Refresh_token,
        IP_fail
    }
}
