using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data.Common;
using System.Data;
using System.Threading.Tasks;
using GTANetworkAPI;

namespace Backend.Modules.MySQL
{
    public class MySQLModule : Script
    {
        private static string host = "localhost";
        private static string user = "root";
        private static string pass = "";
        private static string db = "";

        public static string Connection = "SERVER=" + host + "; DATABASE=" + db + "; UID=" + user + "; PASSWORD=" + pass + ";";
        public static bool Debug = false;

        [ServerEvent(Event.ResourceStart)]
        public void OnResourceStart()
        {
            Console.WriteLine("[MySQL] Testing MySQL Wrapper");
            Test();
        }

        public static bool Test()
        {
            Console.WriteLine("[MYSQL] Testing connection...");
            try
            {
                using (MySqlConnection conn = new MySqlConnection(Connection))
                {
                    conn.Open();
                    Console.WriteLine("[MYSQL] Connection is successful!");
                    conn.Close();
                }
                return true;
            }
            catch (ArgumentException)
            {
                Console.WriteLine($"[MYSQL] Сonnection string contains an error");
                return false;
            }
            catch (MySqlException me)
            {
                switch (me.Number)
                {
                    case 1042:
                        Console.WriteLine("[MYSQL] Unable to connect to any of the specified MySQL hosts");
                        break;
                    case 0:
                        Console.WriteLine("[MYSQL] Access denied");
                        break;
                    default:
                        Console.WriteLine($"[MYSQL] ({me.Number}) {me.Message}");
                        break;
                }
                return false;
            }
        }
        public static void Query(MySqlCommand command)
        {
            try
            {
                Console.WriteLine("[MYSQL] Query to DB:\n" + command.CommandText);

                using (MySqlConnection connection = new MySqlConnection(Connection))
                {
                    connection.Open();

                    command.Connection = connection;

                    command.ExecuteNonQuery();
                }
               
            }
            catch (Exception e) { Console.WriteLine(e.ToString()); }
        }

        public static void Query(string command)
        {
            using (MySqlCommand cmd = new MySqlCommand(command))
            {
                Query(cmd);
            }
        }

        public static async Task QueryAsync(MySqlCommand command)
        {
            try
            {
                if (Debug) Console.WriteLine("[MYSQL] Query to DB:\n" + command.CommandText);
                using (MySqlConnection connection = new MySqlConnection(Connection))
                {
                    await connection.OpenAsync();

                    command.Connection = connection;

                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (Exception e) { Console.WriteLine(e.ToString()); }
        }

        public static async Task QueryAsync(string command)
        {
            try
            {
                if (Debug) Console.WriteLine("[MYSQL] Query to DB:\n" + command);
                using (MySqlConnection connection = new MySqlConnection(Connection))
                {
                    await connection.OpenAsync();

                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = connection;
                        cmd.CommandText = command;

                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception e) { Console.WriteLine(e.ToString()); }
        }

        public static DataTable QueryRead(MySqlCommand command)
        {
            if (Debug) Console.WriteLine("[MYSQL] Query to DB:\n" + command.CommandText);
            using (MySqlConnection connection = new MySqlConnection(Connection))
            {
                connection.Open();

                command.Connection = connection;

                DbDataReader reader = command.ExecuteReader();
                DataTable result = new DataTable();
                result.Load(reader);

                return result;
            }
        }

        public static DataTable QueryRead(string command)
        {
            using (MySqlCommand cmd = new MySqlCommand(command))
            {
                return QueryRead(cmd);
            }
        }

        public static async Task<DataTable> QueryReadAsync(MySqlCommand command)
        {
            if (Debug) Console.WriteLine("[MYSQL] Query to DB:\n" + command.CommandText);
            using (MySqlConnection connection = new MySqlConnection(Connection))
            {
                await connection.OpenAsync();

                command.Connection = connection;

                DbDataReader reader = await command.ExecuteReaderAsync();
                DataTable result = new DataTable();
                result.Load(reader);

                return result;
            }
        }

        public static async Task<DataTable> QueryReadAsync(string command)
        {
            using (MySqlCommand cmd = new MySqlCommand(command))
            {
                return await QueryReadAsync(cmd);
            }
        }

        public static string ConvertTime(DateTime dateTime)
        {
            return dateTime.ToString("s");
        }
    }
}