﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using Tourism.ApiAuthen.Models.ExcHandler;

namespace Tourism.ApiAuthen.CustomnAttributes
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