using GTANetworkAPI;
using MySql.Data.MySqlClient;
using Backend.Core.Extensions;
using Backend.Core.Models;
using Backend.Handlers.Admin.Models;
using Backend.Modules.Admin;
using Backend.Modules.MySQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Backend.Handlers.Login
{
    class AdminHandler : Script

    {
        public static AdminHandler Instance { get; } = new AdminHandler();


        public async Task<List<SupportRankModel>> GetSupportRanks()
    {
        MySqlCommand command = new MySqlCommand("SELECT * FROM `support_ranks`");

        DataTable result = await MySQLModule.QueryReadAsync(command);
        if (result == null || result.Rows.Count == 0) return null;

            List<SupportRankModel> supportRankModels = new List<SupportRankModel>();

            foreach (DataRow row in result.Rows)
        {
            supportRankModels.Add(new SupportRankModel((int) row["id"], (string) row["name"], (int) row["color_r"],
                (int) row["color_g"], (int) row["color_b"], (int) row["permission"]));
            Console.WriteLine(JsonConvert.SerializeObject(supportRankModels));
        }
            return supportRankModels; 
    }

    
        public SupportRankModel GetSupportRankModelById(int id) => AdminModule.Instance.SupportRanks.Find(i => i.id == id);
        
    }
}
