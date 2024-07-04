using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace QuizFlash
{
    public class Database : IDisposable
    {
        //private readonly string remoteConnection = "server=192.168.100.6;user=root;database=quizflash;password=";
        private readonly string remoteConnection = "server=mysqlservice-muzamilsahab05-ede7.g.aivencloud.com;user=avnadmin;database=QuizFlash;password=AVNS_zXdNb0qawMfqWPWYsS8;port=23609";
        private readonly string connectionString = "server=127.0.0.1;user=root;database=quizflash;password=";

        private MySqlConnection connection;

        public Database()
        {
            connection = new MySqlConnection(remoteConnection);
        }

        // Open the database connection
        private void OpenConnection()
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        // Close the database connection
        private void CloseConnection()
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }

        // Execute a non-query command (INSERT, UPDATE, DELETE)
        public int ExecuteNonQuery(string query, params MySqlParameter[] parameters)
        {
            OpenConnection();
            using (var cmd = new MySqlCommand(query, connection))
            {
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                int result = cmd.ExecuteNonQuery();
                CloseConnection();
                return result;
            }
        }

        // Execute a scalar command (single value result)
        public object ExecuteScalar(string query, params MySqlParameter[] parameters)
        {
            OpenConnection();
            using (var cmd = new MySqlCommand(query, connection))
            {
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                object result = cmd.ExecuteScalar();
                CloseConnection();
                return result;
            }
        }

        // Execute a query and return a DataTable
        public DataTable ExecuteQuery(string query, params MySqlParameter[] parameters)
        {
            OpenConnection();
            using (var cmd = new MySqlCommand(query, connection))
            {
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                using (var adapter = new MySqlDataAdapter(cmd))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    CloseConnection();
                    return dataTable;
                }
            }
        }

        public void Dispose()
        {
            if (connection != null)
            {
                connection.Dispose();
                connection = null;
            }
        }
    }
}
