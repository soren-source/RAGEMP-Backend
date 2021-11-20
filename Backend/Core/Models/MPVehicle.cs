using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Core.Models
{
    class MPVehicle
    {
        public enum VehicleTypes
        {
            PLAYER,
            TEAM,
            ADMIN
        }

        public int DbId { get; set; }
        public int OwnerId { get; set; }
        public string VehicleHash { get; set; }
        public Vehicle Vehicle { get; set; }

        private bool _locked;
        public bool Locked
        {
            get => _locked;
            set
            {
                Vehicle.SetSharedData("lockedStatus", value);
                Vehicle.Locked = value;
                _locked = value;
            }
        }

        private bool _engine;
        public bool Engine
        {
            get => _engine;
            set
            {
                Vehicle.SetSharedData("engineStatus", value);
                Vehicle.EngineStatus = value;
                _engine = value;
            }
        }

        public bool Trunk { get; set; }
        public bool IsTeamVehicle { get; set; }
        public int TeamId { get; set; }
        public int primarycolor { get; set; }
        public int secondarycolor { get; set; }

        public VehicleTypes VehicleType { get; set; } = VehicleTypes.PLAYER;

        public MPVehicle(Vehicle Vehicle)
        {
            this.Vehicle = Vehicle;
            mpPools._vehicles.Add(this);

            Locked = true;
            Engine = false;
        }
        public bool Remove()
        {
            MPVehicle mpV = mpPools._vehicles.Find(x => x.Vehicle != null && x.Vehicle.Handle == Vehicle.Handle);
            if (mpV == null) return false;

            mpPools._vehicles.Remove(mpV);

            return true;
        }
    }
}
