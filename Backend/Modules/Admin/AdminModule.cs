
using GTANetworkAPI;
using Newtonsoft.Json;
using Backend.Core.Models;
using Backend.Handlers.Admin.Models;
using Backend.Handlers.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Core.Extensions;
using Backend.Modules.MySQL;
using MySql.Data.MySqlClient;

namespace Backend.Modules.Admin
{
    class AdminModule : ModuleBase<AdminModule>
    {
        public List<SupportRankModel> SupportRanks = new List<SupportRankModel>();

        public AdminModule()
        {
            Instance.SupportRanks = AdminHandler.Instance.GetSupportRanks().Result;
        }

    }
}
    
