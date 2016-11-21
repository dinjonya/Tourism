using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using System.Data.SqlClient;

namespace DinJonYa.Plugs.Data
{
    public class ConvertDataReaderToList<T>
    {
        public static List<T> Convert(IDataReader dr)
        {
            if (dr == null)
                return null;
            List<T> tList = new List<T>();
            while(dr.Read())
            {
                T t = Activator.CreateInstance<T>();
                ProperSetValue(dr, t);
                tList.Add(t);
            }
            return tList;
        }

        private static void ProperSetValue(IDataReader dr, Object t)
        {
            PropertyInfo[] pary = t.GetType().GetProperties();
            foreach (PropertyInfo p in pary)
            {//�ж��Ƿ���ϵͳ����
                if (p.PropertyType.FullName.StartsWith("System."))
                {                  
                    try
                    {
                        p.SetValue(t,dr.GetValue(dr.GetOrdinal(p.Name)), null);
                        //�ж��Ƿ������ǰ��                       
                    }
                    catch
                    {
                        continue;
                    }
                }
                else
                {
                    //�����Զ������ʹ�������
                    Object obj = Assembly.GetAssembly(p.PropertyType).CreateInstance(p.PropertyType.FullName);
                    p.SetValue(t, obj, null);
                    ProperSetValue(dr, obj);
                }
            }
        }
    }
}
