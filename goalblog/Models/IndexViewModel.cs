using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace goalblog.Models
{
    public class IndexViewModel
    {
        public IEnumerable<New> fews { get; set; }
        public IEnumerable<Class> classes { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}