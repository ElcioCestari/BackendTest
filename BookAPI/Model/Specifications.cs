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
            var teste = this.Illustrator.ToString();
            return "Specifications {" +
                "Originally published: " + this.Originallypublished.ToString() + ", " +
                "Author: " + this.Author.ToString() + ", " +
                "Pegecount: " + this.Pagecount.ToString() + ", " +
                "Ilustrator: " + this.Illustrator.ToString() + ", " +
                "Genres: " + this.Genres.ToString() + 
                "}"
                ;
        }
    }
}
