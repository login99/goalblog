using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace goalblog.Models
{
    public class TrNews
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime date { get; set; }
        [AllowHtml]
        public string Text { get; set; }
        public byte[] View { get; set; }
        public ICollection<Class> Class { get; set; }
        public TrNews()
        {
            Class = new List<Class>();
        }
    }
}