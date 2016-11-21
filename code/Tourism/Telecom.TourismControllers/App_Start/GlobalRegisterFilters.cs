/// <summary> 
///     作者：
///     时间：2016/8/22 15:01:43  
///     部门：  
///     公司：Axon
///     版权：2016-2012  
///  CLR版本：4.0.30319.42000 
///     说明：本代码版权归Axon所有
/// 唯一标识：5aaa555c-7b8e-4344-8f7c-4ac6ffb7ff36 
///
/// 更改作者：
/// 更改说明：
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Telecom.TourismControllers.CustomnAttributes;

namespace Telecom.TourismControllers
{
    public class GlobalRegistrFilters
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute(), 3);
        }
    }
}
