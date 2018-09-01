using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace goalblog.Models
{
    public class ChWorldGropeView
    {
        public IEnumerable<ChWorld> chw { get; set; }
        public IEnumerable<Grope> gropes { get; set; }
    }
}