using Google.Protobuf.WellKnownTypes;
using GTANetworkAPI;
using Backend.Handlers.Admin.Models;
using Backend.Modules.Sync.Clothes.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Backend.Core.Extensions;
using Backend.Modules.MySQL;
using Backend.Modules.Sync.Clothes;
using MySql.Data.MySqlClient;
using Backend.Modules.Team.Data;

namespace Backend.Core.Models
{
    class MPPlayer
    {
        public int Id { get; set; }
       
        public string Name
        {
            get => Player.Name;
            set => Player.Name = value;
        }

        public bool LoggedIn { get; set; } = false;
        public TeamData Team { get; set; }
        public int Money { get; set; } = 500;

        public bool Injured { get; set; } = false;

        public Player Player { get; set; }
        public bool InAduty { get; set; } = false;

        public SupportRankModel SupportRank { get; set; }

        public bool firstLogin { get; set; }
        public int Balance { get; set; }

        public string SocialClub { get; set; }

        public string HWID { get; set; }

        public SyncClothesModel SyncClothes
        {
            get => SyncClothes;
            set {
                try
                {
                    SyncClothes = value;
                    Player.TriggerEvent("setCloth");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }

        public void updateMoney(int money)
        {
            try
            {
                MPPlayer mp = Player.getMPPlayer();
                if (mp == null) return;
                
                MySqlCommand command = new MySqlCommand("UPDATE players SET money = @money WHERE id = @id");
                command.Parameters.AddWithValue("@money", money);
                command.Parameters.AddWithValue("@id", mp.Id);

                MySQLModule.Query(command);
                    
                mp.Player.TriggerEvent("updateMoney", money);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void setCloth(int slot, int drawable, int texture)
        {
            try
            {
                Player.TriggerEvent("setCloth", Player, slot, drawable, texture);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void sendProgressbar(int duration)
        {
            Player.TriggerEvent("sendProgressbar", duration);
        }
        public void TriggerAduty (bool aduty)
        {
            try
            {
                Player.TriggerEvent("updateAduty", aduty);
                Player.TriggerEvent("setPlayerAduty", aduty);

                MPPlayer mp = Player.getMPPlayer();

                if (aduty)
                {
                    SendNotification("Adminduty aktiviert!", 3500, "green", "ADMIN", "");
                    this.setCloth(1, 135, 2);
                    this.setCloth(11, 287, 2);
                    this.setCloth(4, 114, 2);
                    this.setCloth(6, 78, 2);
                    this.setCloth(3, 166, 12);
                    this.setCloth(2, 0, 0);
                    this.setCloth(9, 0, 0);
                }
                else
                {
                    SendNotification("Adminduty deaktiviert!", 3500, "red", "ADMIN", "");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public MPPlayer(Player Player)
        {
            this.Player = Player;
            mpPools._Players.Add(this);
        }

        public void TriggerSmartphone(bool state)
        {
            Player.TriggerEvent("hatNudeln", state);         
        }

        public void SendNotification(string message, int duration ,string color, string title, string bgcolor) 
        {
            Player.TriggerEvent("sendPlayerNotification", message, duration, color, title, bgcolor);
        }

        public bool Remove()
        {
            MPPlayer mp = mpPools._Players.Find(x => x.Player != null && x.Player.Handle == Player.Handle);
            if (mp == null) return false;

            mpPools._Players.Remove(mp);

            return true;
        }
    }
}