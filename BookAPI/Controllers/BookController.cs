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
        /* 
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        */



        private readonly ILogger<BookController> _logger;

        public BookController(ILogger<BookController> logger)
        {
            _logger = logger;
        }


        /**
         * Busca todos os livros
         * 
         * sort - um parametro que indica se a lista de livros devera ser ordenada ou nao
         */
        [HttpGet]
        public IEnumerable<Book> Get(bool sort)
        {
            List<Book> list = new List<Book>();

            try {
                list = this.ReadBooks();
            } catch (Exception e) {
                return null;
            }
            
            
            if (sort) return sortList(list); //lista ordenada 
            else return list;
        
        }

        /**
         * //devolve a lista ordenada pelo price
         */
        private IEnumerable<Book> sortList(List<Book> list)
        {
            return list.OrderBy(book => book.price).ToList(); 
        }

        /**
         * Busca um book pelo id, pelo nome do autor ou pelo nomme do livro.
         * A ordem de busca seguira essa sequencia (id, autor, livro) caso 
         * seja encontrada na 1 busca (id por exemplo) ira retornar o book,
         * caso não encontre seguira para o proximo até encontrar (ou não).
         * 
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
         * Le todos os books salvos no diretorio Data salvo com o nomme books.JSON.
         * return - List contendo todos os books
         */
        private List<Book> ReadBooks()
        {
            String txt = System.IO.File.ReadAllText(System.AppDomain.CurrentDomain.BaseDirectory.ToString() + @"\Data\books.JSON");

            List<Book> listBook = null;
            try
            {
                listBook = JsonSerializer.Deserialize<List<Book>>(txt);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception(e.Message);
            }
            
            return listBook;
        }
    }
}
