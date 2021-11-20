using GTANetworkAPI;
using Backend.Core.Models;
using Backend.Modules.MySQL;
using Backend.Utils;
using Backend.Core.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Core
{

    public abstract class Data
    {
        protected Data(MySqlDataReader reader) { }

        public abstract int GetIdentifier();
    }

    public abstract class DataModule<T, TData> : ModuleBase<T> where T : ModuleBase<T> where TData : Data
    {
        public abstract string Query { get; }

        public Dictionary<int, TData> Items = new Dictionary<int, TData>();

        public abstract void OnLoaded();

        public override void OnLoad()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(MySQLModule.Connection))
                {
                    connection.Open();

                    MySqlCommand command = connection.CreateCommand();
                    command.CommandText = Query;

                    MySqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        object obj = Activator.CreateInstance(typeof(TData), reader);

                        if (obj is TData)
                        {
                            TData data = (TData) obj;
                            try
                            {
                                Items.Add(data.GetIdentifier(), data);
                            }
                            catch
                            {
                            }
                        }
                    }

                    if (Configuration.Instance.DevMode)
                        Log($"Es wurden {Items.Count} {Items.Values.GetType().Name}s geladen.");
                }

                OnLoaded();
            }
            catch (Exception e)
            {
                Log(e.ToString());
            }
        }

        public void Log(string message)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("[DataModule] ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
