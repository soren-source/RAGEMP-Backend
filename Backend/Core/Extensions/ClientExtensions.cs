using GTANetworkAPI;
using Backend.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Backend.Core.Extensions
{
    static class PlayerExtensions
    {
        public static MPPlayer getMPPlayer(this Player Player)
        {
            return mpPools._Players.FirstOrDefault(c => c.Player != null && c.Player.Handle == Player.Handle);
        }
    }
}
