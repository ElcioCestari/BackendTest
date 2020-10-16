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
    }
}
