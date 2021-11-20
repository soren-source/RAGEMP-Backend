using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace Backend.Handlers.Admin.Models
{
    class SupportRankModel
    {
        public SupportRankModel(int id, string name, int color_r, int color_g, int color_b, int permission)
        {
            this.id = id;
            this.name = name;
            this.color_r = color_r;
            this.color_g = color_g;
            this.color_b = color_b;
            this.permission = permission;
        }

        public int id { get; set; }
        public string name { get; set; }
        public int color_r { get; set; }
        public int color_g { get; set; }
        public int color_b { get; set; }
        public int permission { get; set; }
    }
}
