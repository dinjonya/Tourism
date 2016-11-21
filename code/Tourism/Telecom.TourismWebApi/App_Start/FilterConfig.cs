using System.Web;
using System.Web.Mvc;
using Telecom.TourismWebApi.Models;

namespace Telecom.TourismWebApi
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
