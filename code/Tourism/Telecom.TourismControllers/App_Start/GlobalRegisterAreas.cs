/// <summary> 
///     作者：
///     时间：2016/8/5 14:35:27  
///     部门：  
///     公司：Axon
///     版权：2016-2012  
///  CLR版本：4.0.30319.42000 
///     说明：本代码版权归Axon所有
/// 唯一标识：198c0293-7f8d-466c-9baa-7a68ec2815fd 
///
/// 更改作者：
/// 更改说明：
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Telecom.TourismControllers
{
    public class GlobalRegistrAreas
    {
        public static void RegisterArea()
        {
            AreaRegistration.RegisterAllAreas();
        }
    }
}