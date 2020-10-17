using BookAPI.Model;
using System;

namespace BookAPI
{
    public class Book
    {
        public int id { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public Specifications specifications { get; set; }

        public override string ToString()
        {
            return "Book {\n" +
                "id: " + this.id.ToString() +
                "\nname: " + this.name.ToString() +
                "\nprice: " + this.price.ToString() +
                "\n" + this.specifications.ToString() +
                "\n}"
            ;
                
        }
    }
}
