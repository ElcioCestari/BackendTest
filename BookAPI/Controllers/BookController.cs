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
            
            return sort ? sortList(list) : list; 
        }


        /**
         * localiza um book pelo id e Devolve um json com dados basicos desse book e com o frete calculado.
         * 
         * int id - o id do book.
         */
        [Route("/book/frete")]
        public JsonResult Get(int id)
        {
            Book book = searchById(id);

            if (book == null) return null;

            //valor do book acrescido do frete
            double priceWithShipping = calculateShipping(book.price);

            String json = "{ " +
                book.simpleJsonBook() + ", " +
                "{frete : " + priceWithShipping + " } " +
                "}";

            return new JsonResult(json);
        }

        /**
         * devolve a lista ordenada pelo price
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
         * 
         * int id - busca um livro por id
         * String authName - busca livros pelo nome do autor
         * String bookName - busca livros pelo nome do livro
         * bool sort - se true retorna a busca ordenada conforme o metodo sortList (ordena pelo preço)
         */
        [HttpGet("{book}")]
        public JsonResult GetBook(int id, String authName, String bookName, bool sort)
        {
            List<Book> books = null;//lista que ira retornar os books

            //pesquisa um book por id
            if (id != null )
            {
                Book book = searchById(id);
                if (book != null) return new JsonResult(book);

            } 
            
            //pesquisa books pelo nome do autor
            if ( authName != null && authName != "")
            {
                books = searchByAuthName(authName);
                
                //caso exista retorna uma lista de livros ordenada ou nao
                if (books != null && books.Count() > 0 )
                    return sort ?  new JsonResult(this.sortList(books)) : new JsonResult(books);

            } 
            
            //pesquisa books pelo nome do livro
            if (bookName != null && bookName != "")
            {
                books = searchByBookName(bookName);

                //caso exista retorna uma lista de livros ordenada ou nao
                if (books != null && books.Count() > 0)
                    return sort ? new JsonResult(this.sortList(books)) : new JsonResult(books);

            }

            //caso nao encontre nada
            return new JsonResult(
                "nada econtrado com os parametros id: " + id 
                + " authName: " + authName 
                + " bookName: " + bookName);
        }


        /**
         *Busca e retorna livros pelo nome do livro.
         *caso não encontre retorna uma lista vazia
         */
        private List<Book> searchByBookName(string bookName)
        {
            List<Book> books = ReadBooks();
            List<Book> tempList = new List<Book>();

            foreach (var list in books)
            {
                if (list.name.ToString() == bookName) tempList.Add(list);
            }

            return tempList;
        }

        /**
         * Busca e retorna uma lista livro pelo nome do autor
         * caso nao enconte retorna uma lista vazia
         */
        private List<Book> searchByAuthName(string authName)
        {
            List<Book> books = ReadBooks();
            List<Book> tempList = new List<Book>();

            foreach (var list in books)
            {
                if (list.specifications.Author.ToString() == authName) tempList.Add(list);
            }
            return tempList;
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
         * return - List contendo todos os books, caso esteja vazia retorna null
         * 
         * throw - uma exception caso ocorra erro ao ler o arquivo
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


        /**
         * Calcula o valor do frete baseado no valor do livro acrescentando 20% no valor final
         * double price - preço do livro
         */
        private double calculateShipping(double price)
        {
            return price * 1.20;
        }
    }
}
