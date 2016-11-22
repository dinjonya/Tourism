using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using MySql.Data.MySqlClient;
namespace DinJonYa.Plugs.Data.DbUtility
{
    public class MySqlUtility
    {
        private static string connectionString = "";
        static MySqlUtility()
        {
            connectionString = "1";
        }
        public static readonly string connStr = connectionString;
        /*优点：1、把数据库连接代码都放在SQLHelper中，使代码更简洁
             *   2、使用DataTable可以随意读取数据库，而之前做的用户登录使用的SqlReader只能逐行往前读*/

        public static int ExecuteNonQuery(string sql, params MySqlParameter[] parameters)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    foreach (MySqlParameter parameter in parameters)
                    {
                        cmd.Parameters.Add(parameter);
                    }
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public static int ExecuteNonQueryTran(List<string> cmdTextList,List<MySqlParameter[]> parameterList)
        {
            using (MySqlConnection conn =new MySqlConnection(connStr))
            {
                if(conn.State!=ConnectionState.Open) conn.Open();
                using (MySqlTransaction tran = conn.BeginTransaction())
                {
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        int sum = 0;
                        try
                        {
                            for (int i = 0; i < cmdTextList.Count; i++)
                            {
                                cmd.CommandText = cmdTextList[i];
                                if (parameterList != null && parameterList[i]!=null)
                                {
                                    foreach (MySqlParameter parameter in parameterList[i])
                                    {
                                        cmd.Parameters.Add(parameter);
                                    }
                                }
                                sum = sum + cmd.ExecuteNonQuery();
                            }
                            tran.Commit();
                            return sum;
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                            throw ex;
                        }
                    }
                }
            }
        }

        public static object ExecuteScalar(string sql, params MySqlParameter[] parameters)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    foreach (MySqlParameter parameter in parameters)
                    {
                        cmd.Parameters.Add(parameter);
                    }
                    //执行查询，并返回查询所返回的结果集中第一行的第一列。忽略其它的行或列。
                    return cmd.ExecuteScalar();
                }
            }
        }
        public static MySqlDataReader ExecuteReader(string sql, params MySqlParameter[] parameters)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    foreach (MySqlParameter parameter in parameters)
                    {
                        cmd.Parameters.Add(parameter);
                    }
                    return cmd.ExecuteReader();
                }
            }
        }
        public static DataTable ExecuteDataTable(string sql, params MySqlParameter[] parameters)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    foreach (MySqlParameter parameter in parameters)
                    {
                        cmd.Parameters.Add(parameter);
                    }
                    //执行查询，并返回查询所返回的结果集中第一行的第一列。忽略其它的行或列。
                    DataSet dataset = new DataSet();
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(dataset);
                    return dataset.Tables[0];
                }
            }
        }
    }
}
