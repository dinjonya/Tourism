using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Telecom.TourismWebApi.ApiControllers
{
    public class ValuesController : ApiController
    {
        [Route("api/test")]
        [HttpGet]
        // GET api/values
        public string Get()
        {
            int a = 0;
            int b = 0;
            int c = a / b;
            return "api-controller-value";
        }
    }
}
