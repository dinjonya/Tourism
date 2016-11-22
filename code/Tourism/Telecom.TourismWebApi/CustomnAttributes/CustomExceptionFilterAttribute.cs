using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using Telecom.TourismWebApi.Models.ExcHandler;

namespace Telecom.TourismWebApi.CustomnAttributes
{
    public class CustomExceptionFilterAttribute : System.Web.Http.Filters.ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var ex = actionExecutedContext.Exception;
            ExHandler.TreatedWriteLog(ex, ex.Message);

        }
    }
}