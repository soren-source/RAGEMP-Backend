using GTANetworkAPI;
using Backend.Core.Extensions;
using Backend.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backend.Core.Models
{
    class ModuleController : Script
    {
        private IEnumerable<Module> _modules;
        public ModuleController()
        {
            _modules = typeof(Module)
                             .Assembly.GetTypes()
                             .Where(t => t.IsSubclassOf(typeof(Module)) && !t.IsAbstract)
                             .Select(t => (Module)Activator.CreateInstance(t));
        }

        [RemoteEvent]
        public void Pressed_E(Player Player)
        {
            if (Player.IsNull || Player == null) return;
            _modules.ForEach(e =>
            {
                if ( e.OnKeyPress(Player, Utils.KeyEnumeration.E)) return;
            });
        }

        /*[ServerEvent(Event.PlayerDeath)]
        public async void OnPlayerDeath(Player Player, Player killer, uint reason)
        {
            if (Player.IsNull || Player == null) return;
            if (killer.IsNull || killer == null) killer = Player;

            MPPlayer mp = Player.getMPPlayer();
            if (mp == null || mp.Injured) return;

            mp.Injured = true;

            await _modules.ForEach(async e =>
            {
                if (await e.OnPlayerDeath(Player, killer, reason)) return;
            });
        }*/

        [ServerEvent(Event.PlayerConnected)]
        public async void OnPlayerConnected(Player Player)
        {
            if (Player.IsNull || Player == null) return;

            await _modules.ForEach(async e =>
            {
                if (await e.OnPlayerConnect(Player)) return;
            });
        }

        [ServerEvent(Event.ResourceStart)]
        public async void OnLoad()
        {
            await _modules.ForEach(async e =>
            {
                e.OnLoad();
                await e.OnLoadAsync();

            });
        }
    }
}
