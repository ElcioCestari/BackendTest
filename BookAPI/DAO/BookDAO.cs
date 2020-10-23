using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace BookAPI.DAO
{
    public class BookDAO
    {
        private string booksJSON;
        private const string fileName = "books.JSON";
        private const string directoryName = @"\Data\";

        public BookDAO()
        {
            this.setBooksJASON();  
        }

        /// <summary>
        /// Configura booksJSON com todos os books que estão salvos em um arquivo json.
        /// </summary>
        private void setBooksJASON()
        {
            try
            {
                this.booksJSON = System.IO.File
                    .ReadAllText(System.AppDomain.CurrentDomain.BaseDirectory.ToString() + directoryName + fileName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception("Erro ao ler arquivo");
            }
        }

        /// <summary>
        /// Esse metodo busca todos os livros
        /// </summary>
        /// <returns> List - contendo todos os livros</returns>
        /// <exception cref="Exception">Quando nao consegue converter json para List<Book> </exception>
        public List<Book> selectAll()
        {
            List<Book> listBook = null;
            try
            {
                listBook = JsonSerializer.Deserialize<List<Book>>(this.booksJSON);
            }
            catch (Exception e)
            {
                throw new Exception("erro ao converter json para books");
            }

            return listBook;
        }

        /// <summary         
        /// Busca um livro pelo id do livro  caso não encontre retorna null
        /// </summary>
        /// <param name="id"> int - representa o id do book</param>
        /// <returns> Book - contendo o book que corresponde ao id </returns>
        public Book searchById(int id)
        {
          
            List<Book> books = this.selectAll();

            foreach (var list in books)
            {
                if (list.id == id) return list;
            }
            return null;
        }

        ///<summary>
        /// Busca e retorna uma lista livro pelo nome do autor 
        /// caso nao enconte retorna uma lista vazia
        /// </summary>
        public List<Book> searchByAuthName(string authName)
        {
            List<Book> books = this.selectAll();
            List<Book> tempList = new List<Book>();

            foreach (var list in books)
            {
                if (list.specifications.Author.ToString() == authName) tempList.Add(list);
            }
            return tempList;
        }

        ///<summary>
        ///Busca e retorna livros pelo nome do livro. caso não encontre retorna uma lista vazia 
        /// </summary>
        public List<Book> searchByBookName(string bookName)
        {
            List<Book> books = this.selectAll();
            List<Book> tempList = new List<Book>();

            foreach (var list in books)
            {
                if (list.name.ToString() == bookName) tempList.Add(list);
            }

            return tempList;
        }
    }
}
