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
            return "Book: {" +
                "id: " + this.id.ToString() + "," +
                "name: " + this.name.ToString() + "," +
                "price: " + this.price.ToString() + "," +
                "" + this.specifications.ToString() +
                "}"
            ;
        }

        public string simpleJsonBook()
        {
            return "Book: {" +
                " id: " + this.id.ToString() + "," +
                "name: " + this.name.ToString() + "," +
                "price: " + this.price.ToString() +
                " }";
        }
    }
}
