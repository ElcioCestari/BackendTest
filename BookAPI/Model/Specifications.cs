    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookAPI.Model
{
    public class Specifications
    {
        public String Originally_published { get; set; }
        public string Author { get; set; }
        public int Pagecount { get; set; }
        public object Illustrator { get; set; }
        public object Genres { get; set; }
    }
}
