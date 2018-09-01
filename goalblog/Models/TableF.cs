using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.Models
{
    public class TableF
    {
        public int Id { get; set; }
        public int NumberTable { get; set; }
        public byte[] Icon { get; set; }
        public string Name { get; set; }
        public int Game { get; set; }
        public int WinGame { get; set; }
        public int DrawGame { get; set; }
        public int LoseGame { get; set; }
        public int Goal { get; set; }
        public int MissGoal { get; set; }
        public int Score { get; set; }
        public ICollection<Class> Classes { get; set; }
        public TableF()
        {
            Classes = new List<Class>();
        }
    }
}