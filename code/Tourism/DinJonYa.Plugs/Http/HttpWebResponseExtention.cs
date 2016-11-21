using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace DinJonYa.Plugs.Http
{
    public static class HttpWebResponseExtention
    {
        /// <summary>
        /// 获取string返回值
        /// </summary>
        /// <param name="resp"></param>
        /// <returns></returns>
        public static string GetString(this HttpWebResponse resp)
        {
            using (var stream = resp.GetResponseStream())
            {
                using (var sr = new StreamReader(stream, Encoding.UTF8))
                {
                    var htmlString = sr.ReadToEnd();
                    return htmlString;
                }
            }
        }
    }
}
