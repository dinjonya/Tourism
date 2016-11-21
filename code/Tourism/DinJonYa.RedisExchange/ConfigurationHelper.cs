/// <summary> 
///     作者：
///     时间：2016/9/21 17:26:47  
///     部门：  
///     公司：Axon
///     版权：2016-2012  
///  CLR版本：4.0.30319.42000 
///     说明：本代码版权归Axon所有
/// 唯一标识：b1857be3-b7b1-4dae-95f0-773cf47389d5 
///
/// 更改作者：
/// 更改说明：
/// </summary>

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinJonYa.RedisExchange
{
    public static class ConfigurationHelper
    {
        internal static T Get<T>(string appSettingsKey, T defaultValue)
        {
            string text = ConfigurationManager.AppSettings[appSettingsKey];
            if (string.IsNullOrWhiteSpace(text))
                return defaultValue;
            try
            {
                var value = Convert.ChangeType(text, typeof(T));
                return (T)value;
            }
            catch
            {
                return defaultValue;
            }
        }
    }
}
