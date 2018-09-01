using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Models
{
    [Table("New")]
    public class New
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime date { get; set; }
        public string Text { get; set; }
        public byte[] View { get; set; }
        public ICollection<Class> Class { get; set; } 
        public New()
        {
            Class = new List<Class>();
        }
    }
}