/// <summary> 
///     作者：
///     时间：2016/9/5 18:26:55  
///     部门：  
///     公司：Axon
///     版权：2016-2012  
///  CLR版本：4.0.30319.42000 
///     说明：本代码版权归Axon所有
/// 唯一标识：ba74b4df-fe2c-49e4-bb89-28239004761b 
///
/// 更改作者：
/// 更改说明：
/// </summary>

using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinJonYa.Plugs.EntityHelper
{
    public static class EFHelper
    {
        public static void EFUpdate<T>(this DbEntityEntry<T> t) where T : class
        {

        }
    }
}
