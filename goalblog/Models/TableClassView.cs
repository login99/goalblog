using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace goalblog.Models
{
    public class TableClassView
    {
        public IEnumerable<TableF> tables { get; set; }
        public IEnumerable<Class> classes { get; set; }
    }
}