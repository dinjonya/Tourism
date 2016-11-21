using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;

namespace DinJonYa.Plugs.Data
{
    public class ConvertDataSetToList<T>
    {
        public static List<T> Convert(DataSet ds, int tableIndex)
        {
            if (ds == null || ds.Tables.Count == 0 || ds.Tables.Count < tableIndex)
                return null;
            List<T> tList = new List<T>();
            foreach (DataRow dr in ds.Tables[tableIndex].Rows)
            {
                T t = Activator.CreateInstance<T>();
                ProperSetValue(dr, t);
                tList.Add(t);
            }
            return tList;
        }

        private static void ProperSetValue(DataRow dr, Object t)
        {
            PropertyInfo[] pary = t.GetType().GetProperties();
            foreach (PropertyInfo p in pary)
            {//判断是否是系统类型
                if (p.PropertyType.ToString().StartsWith("System."))
                {//判断是否包含当前列
                    if (dr.Table.Columns.Contains(p.Name))
                    {
                        if (!(dr[p.Name] is DBNull))
                            p.SetValue(t, dr[p.Name], null);
                        else
                            p.SetValue(t, null, null);
                    }
                }
                else
                {
                    //按照自定义类型创建对象
                    Object obj = Assembly.GetAssembly(p.PropertyType).CreateInstance(p.PropertyType.FullName);
                    p.SetValue(t, obj, null);
                    ProperSetValue(dr, obj);
                }
            }
        }
    }
}
