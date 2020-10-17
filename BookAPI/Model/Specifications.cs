    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BookAPI.Model
{
    public class Specifications
    {
        [JsonPropertyName("Originally published")]
        public String Originallypublished { get; set; }
        public string Author { get; set; }
        public int Pagecount { get; set; }
        public object Illustrator { get; set; }
        public object Genres { get; set; }

        public override string ToString()
        {
            return "Specifications {" +
                "\nOriginally published: " + this.Originallypublished.ToString() + 
                "\nAuthor: " + this.Author.ToString() + 
                "\nPegecount: " + this.Pagecount.ToString() + 
                "\nIlustrator: " + this.Illustrator.ToString() + 
                "\nGenres: " + this.Genres.ToString() + 
                "\n}"
                ;
        }
    }
}
