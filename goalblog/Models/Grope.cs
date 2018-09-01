using System.Collections.Generic;

namespace goalblog.Models
{
    public class Grope
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ChWorld> chw { get; set; }
        public Grope()
        {
            chw = new List<ChWorld>();
        }
    }
}