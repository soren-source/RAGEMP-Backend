using GTANetworkAPI;
using Backend.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backend.Core.Extensions
{
    static class VehicleExtensions
    {
        public static MPVehicle getmpVehicle(this Vehicle vehicle)
        {
            return mpPools._vehicles.FirstOrDefault(v => v.Vehicle != null && v.Vehicle.Handle == vehicle.Handle);
        }
    }
}
