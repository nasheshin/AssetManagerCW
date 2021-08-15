using System;
using System.Data;
using System.Data.SqlClient;

namespace AssetManager
{
    public static class DataConnector
    {
        public static SqlConnection Connection;
        public static ConnectionState State;

        public static void OpenConnection(string connectionString)
        {
            try
            {
                Connection = new SqlConnection(connectionString);
                Connection.Open();
                State = ConnectionState.Open;
            }
            catch
            {
                State = ConnectionState.Broken;
            }
        }

        public static void CloseConnection()
        {
            try
            {
                Connection.Close();
                State = ConnectionState.Closed;
            }
            catch
            {
                State = ConnectionState.Broken;
            }
        }
    }
}