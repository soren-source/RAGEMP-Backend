using Backend.Core;
using Backend.Core.Models;
using Backend.Modules.Team.Data;
using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Modules.Team
{
    public class TeamModule : DataModule<TeamModule, TeamData>
    {

        public override string Query => "SELECT * FROM `teams`";

        public static List<TeamData> _teams = new List<TeamData>();
        public static Dictionary<Player, TeamData> _members = new Dictionary<Player, TeamData>();

        public override void OnLoaded()
        {
            try
            {
                Items.ForEach(t =>
                {
                    Blip b = NAPI.Blip.CreateBlip(436, t.Value.position, 1f, t.Value.blipcolor, t.Value.name);
                    b.ShortRange = true;
                    b.Dimension = 0;
                    _teams.Add(t.Value);
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
