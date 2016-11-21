using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tourism.ApiAuthen.Models
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class NoAuthentication : Attribute
    {
    }
}