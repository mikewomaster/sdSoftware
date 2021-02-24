namespace HslCommunication.BasicFramework
{
    using System;
    using System.Data.SqlClient;

    public interface ISqlDataType
    {
        void LoadBySqlDataReader(SqlDataReader sdr);
    }
}

