namespace HslCommunication.BasicFramework
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public static class SoftSqlOperate
    {
        public static int ExecuteSelectCount(SqlConnection conn, string cmdStr)
        {
            using (SqlCommand command = new SqlCommand(cmdStr, conn))
            {
                int num = 0;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    num = Convert.ToInt32(reader[0]);
                }
                reader.Close();
                return num;
            }
        }

        public static int ExecuteSelectCount(string conStr, string cmdStr)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                connection.Open();
                return ExecuteSelectCount(connection, cmdStr);
            }
        }

        public static List<T> ExecuteSelectEnumerable<T>(SqlConnection conn, string cmdStr) where T: ISqlDataType, new()
        {
            List<T> list2;
            using (SqlCommand command = new SqlCommand(cmdStr, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    List<T> list = new List<T>();
                    while (reader.Read())
                    {
                        T item = Activator.CreateInstance<T>();
                        item.LoadBySqlDataReader(reader);
                        list.Add(item);
                    }
                    list2 = list;
                }
            }
            return list2;
        }

        public static List<T> ExecuteSelectEnumerable<T>(string conStr, string cmdStr) where T: ISqlDataType, new()
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                connection.Open();
                return ExecuteSelectEnumerable<T>(connection, cmdStr);
            }
        }

        public static T ExecuteSelectObject<T>(SqlConnection conn, string cmdStr) where T: ISqlDataType, new()
        {
            T local2;
            using (SqlCommand command = new SqlCommand(cmdStr, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        T local = Activator.CreateInstance<T>();
                        local.LoadBySqlDataReader(reader);
                        return local;
                    }
                    local2 = default(T);
                }
            }
            return local2;
        }

        public static T ExecuteSelectObject<T>(string conStr, string cmdStr) where T: ISqlDataType, new()
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                connection.Open();
                return ExecuteSelectObject<T>(connection, cmdStr);
            }
        }

        public static DataTable ExecuteSelectTable(SqlConnection conn, string cmdStr)
        {
            DataTable table;
            using (SqlDataAdapter adapter = new SqlDataAdapter(cmdStr, conn))
            {
                using (DataSet set = new DataSet())
                {
                    adapter.Fill(set);
                    table = set.Tables[0];
                }
            }
            return table;
        }

        public static DataTable ExecuteSelectTable(string conStr, string cmdStr)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                connection.Open();
                return ExecuteSelectTable(connection, cmdStr);
            }
        }

        public static int ExecuteSql(SqlConnection conn, string cmdStr)
        {
            using (SqlCommand command = new SqlCommand(cmdStr, conn))
            {
                return command.ExecuteNonQuery();
            }
        }

        public static int ExecuteSql(string conStr, string cmdStr)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                connection.Open();
                return ExecuteSql(connection, cmdStr);
            }
        }
    }
}

