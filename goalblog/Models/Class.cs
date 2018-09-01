using goalblog.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    [Table("Class")]
    public class Class
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<New> News { get; set; }
        public ICollection<TrNews> TrNewses { get; set; }
        public ICollection<TableF> Tableses { get; set; }
        public Class()
        {
            News = new List<New>();
            Tableses = new List<TableF>();
            TrNewses = new List<TrNews>();
        }
    }
}