using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BookAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<BookController> _logger;

        public BookController(ILogger<BookController> logger)
        {
            _logger = logger;
        }


        /**
         * Busca todos os livros
         */
        [HttpGet]
        public IEnumerable<Book> Get()
        {
            /*
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new Book
            {
                id = 1,
                name = "meu livro favorito",
                price = 56.25
            })
            .ToArray();
        */
            return this.ReadBooks();
        
        }

        /**
         * Busca um book pelo id, pelo nome do autor ou pelo nomme do livro.
         * caso encontre retorna um JSON contento as informações desse book
         * caso nao encontre retorna uma string dizendo que não encontrou.
         */
        [HttpGet("{book}")]
        public JsonResult GetBook(int id, String authName, String bookName)
        {
            Book book = null;
            if (id != null )
            {
                book = searchById(id);
                if (book != null) return new JsonResult(book);

            } if ( authName != null && authName != "")
            {
                book = searchByAuthName(authName);
                if (book != null) return new JsonResult(book);

            } if (bookName != null && bookName != "")
            {
                book = searchByBookName(bookName);
                if (book != null) return new JsonResult(book);
            }

            return new JsonResult(
                "nada econtrado com os parametros id: " + id 
                + " authName: " + authName 
                + " bookName: " + bookName);
        }

        /**
         *Busca um livro pelo nome do livro.
         *caso não encontre retorna null
         */
        private Book searchByBookName(string bookName)
        {
            List<Book> books = ReadBooks();

            foreach (var list in books)
            {
                if (list.name == bookName) return list;
            }
            return null;
        }


        /**
         * Busca um livro pelo nome do autor
         * caso não encontre retorna null
         */
        private Book searchByAuthName(string authName)
        {
            List<Book> books = ReadBooks();

            foreach (var list in books)
            {
                if (list.specifications.Author == authName) return list;
            }
            return null;
        }


        /**
         * Busca um livro pelo id do livro
         * caso não encontre retorna null
         */
        private Book searchById(int id)
        {

            List<Book> books = ReadBooks();

            foreach (var list in books)
            {
                if (list.id == id) return list;
            }
            return null;
        }

        /**
         * return - List contendo todos os books
         */
        private List<Book> ReadBooks()
        {
            String txt = System.IO.File.ReadAllText(System.AppDomain.CurrentDomain.BaseDirectory.ToString() + @"\Data\books.JSON");

            List<Book> listBook = null;
            try
            {
                Console.WriteLine(txt);
                listBook = JsonSerializer.Deserialize<List<Book>>(txt);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return listBook;
        }
    }
}
