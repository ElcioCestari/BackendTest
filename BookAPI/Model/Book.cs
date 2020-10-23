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


        ///<summary>
        /// calcula o preço do frete do livro adicionando 20% ao valor final
        /// </summary>
        /// <returns> double - contendo o valor do frete</returns>
        public double calculateShipping()
        {
            return this.price * 1.20;
        }

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
