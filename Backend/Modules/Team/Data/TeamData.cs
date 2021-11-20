using GTANetworkAPI;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Modules.Team.Data
{
    public partial class TeamData : Core.Data
    {
        public TeamData(MySqlDataReader reader) : base(reader)
        {
            id = reader.GetInt32("id");
            name = reader.GetString("name");
            shortname = reader.GetString("shortname");
            blipcolor = reader.GetByte("color");

            posX = reader.GetFloat("posX");
            posY = reader.GetFloat("posY");
            posZ = reader.GetFloat("posZ");
        }

        public override int GetIdentifier() => id;

        public int id { get; set; }
        public string name { get; set; }
        public string shortname { get; set; }
        public byte blipcolor { get; set; }
        public float posX { get; set; }
        public float posY { get; set; }
        public float posZ { get; set; }
    }
    public partial class TeamData
    {
        public Vector3 position;
    }
}
