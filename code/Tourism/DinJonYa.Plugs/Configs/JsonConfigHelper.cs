using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace DinJonYa.Plugs.Configs
{
    public class JsonConfigHelper
    {
        public static T GetConfiguration<T>()
        {
            try
            {
                string configStrings = null;
                using (FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("~/Models/config.json"),FileMode.Open,FileAccess.Read))
                {
                    using (StreamReader str = new StreamReader(fs,Encoding.UTF8))
                    {
                        configStrings = str.ReadToEnd();
                        str.Close();
                    }
                    fs.Close();
                }
                T t = JsonConvert.DeserializeObject<T>(configStrings);
                return t;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
