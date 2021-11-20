using GTANetworkAPI;
using Backend.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Handlers.Vehicle
{
    public class VehicleHandler
    {
        public static void CreateVehicle(string VehicleHash, Vector3 position, float rotation, int DbId, int OwnerId, int primaryColor, int secondaryColor, string numberplate, bool IsTeamVehicle, int TeamId)
        {
            GTANetworkAPI.Vehicle veh = NAPI.Vehicle.CreateVehicle(NAPI.Util.VehicleNameToModel(VehicleHash), position, rotation, primaryColor, secondaryColor, numberplate);
            veh.NumberPlate = numberplate;
            MPVehicle mpVeh = new MPVehicle(veh);

            mpVeh.DbId = DbId;
            mpVeh.OwnerId = OwnerId;
            mpVeh.VehicleHash = VehicleHash;
            mpVeh.Vehicle = veh;
            mpVeh.Locked = true;
            mpVeh.Engine = false;
            mpVeh.primarycolor = primaryColor;
            mpVeh.secondarycolor = secondaryColor;
            mpVeh.IsTeamVehicle = IsTeamVehicle;
            mpVeh.TeamId = TeamId;
        }
    }
}
