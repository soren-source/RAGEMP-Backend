using GTANetworkAPI;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Modules.Garage.Data
{
    public partial class GarageData : Core.Data
    {
        public GarageData(MySqlDataReader reader) : base(reader)
        {
            id = reader.GetInt32("id");
        }

        public override int GetIdentifier() => id;
        public int id { get; set; }
        public Vector3 position { get; set; }
    }

    public partial class GarageData
    {

    }
}
