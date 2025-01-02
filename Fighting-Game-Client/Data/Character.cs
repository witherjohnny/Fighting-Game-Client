using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fighting_Game_Client.Data
{
    internal class Character
    {
        public string Name { get; set; }
        public List<string> BaseActions { get; set; } = new List<string>();
        public List<string> Attacks { get; set; } = new List<string>();
        public List<string> Projectiles { get; set; } = new List<string>();
    }
}
