using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DinJonYa.Plugs.Http;

namespace DinJonYa.Plugs.Mobile
{
    public class MobileHelper
    {
        /// <summary>
        /// 淘宝接口  查询号码归属地
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public static ZoneResult GetMobileZoneResultCatName(string mobile)
        {
            try
            {
                string str = HttpUtil.Get(string.Format(ConfigurationManager.AppSettings["mobile_Tel_Segment"], mobile), "text/plain", "Mozilla-Firefox-Spider(Axon)", "gbk");
                str = str.Split('=')[1];
                Console.WriteLine(str);
                ZoneResult result = Newtonsoft.Json.JsonConvert.DeserializeObject<ZoneResult>(str);
                return result;
            }
            catch (Exception)
            {
                //应当邮件通知错误
                return null;
            }
        }
    }
    public class ZoneResult
    {
        public string mts { get; set; }
        public string province { get; set; }
        public string catName { get; set; }
        public string telString { get; set; }
        public string areaVid { get; set; }
        public string ispVid { get; set; }
        public string carrier { get; set; }
    }
}
