using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace goalblog.Models
{
    public class ChWorld
    {
        public int Id { get; set; }
        public int NumbGr { get; set; }
        public byte[] Icon { get; set; }
        public string Name { get; set; }
        public int Game { get; set; }
        public int WinGame { get; set; }
        public int DrawGame { get; set; }
        public int LoseGame { get; set; }
        public int Goal { get; set; }
        public int MissGoal { get; set; }
        public int Score { get; set; }
        public int? GropeId { get; set; }
        public Grope grope { get; set; }
    }
}