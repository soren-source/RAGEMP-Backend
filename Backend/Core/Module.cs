using GTANetworkAPI;
using Backend.Utils;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Core.Models
{
    public class Module : Script
    {
        public virtual bool OnKeyPress(Player Player, KeyEnumeration key)
        {
            return false;
        }

        public virtual Task<bool> OnPlayerDeath(Player Player, Player killer, uint reason)
        {
            return Task.FromResult(false);
        }

        public virtual Task<bool> OnPlayerConnect(Player Player)
        {
            return Task.FromResult(false);
        }

        public virtual void OnLoad() { }
        public virtual async Task OnLoadAsync() { await Task.FromResult(false); }

    }

    public abstract class ModuleBase<T> : Module where T : ModuleBase<T>
    {
        public static T Instance { get; private set; }

        public ModuleBase()
        {
            Instance = (T)this;
        }
    }
}
