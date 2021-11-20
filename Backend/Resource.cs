using GTANetworkAPI;
using GTANetworkMethods;
using Backend.Core;
using Backend.Core.Extensions;
using Backend.Core.Models;
using Backend.Modules;
using Backend.Modules.MySQL;
using Backend.Utils;
using Backend.Core;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Timers;
using Player = GTANetworkAPI.Player;
using RAGE1._1GW.Handlers.Login;

namespace Backend
{
    public class Resource : Script
    {
        
        [ServerEvent(Event.ResourceStart)]
        public void OnResourceStart()
        {
            Console.WriteLine("CREDITS: SOREN");
            Configuration.Instance.Resource = NAPI.Resource.GetThisResource(this);
        }

    }
}