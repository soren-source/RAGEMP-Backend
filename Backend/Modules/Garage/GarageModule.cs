using Backend.Core;
using Backend.Core.Models;
using Backend.Modules.Garage.Data;
using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Modules.Garage
{
    public class GarageModule : DataModule<GarageModule, GarageData>
    {
        public override string Query => "SELECT * FROM garages";

        public override void OnLoaded()
        {
            try
            {
                Items.ForEach(g =>
                {
                    Blip b = NAPI.Blip.CreateBlip(50, g.Value.position, 1f, 0);
                    b.Name = "Garage : " + b.Id;
                    b.ShortRange = true;
                });
            } catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
