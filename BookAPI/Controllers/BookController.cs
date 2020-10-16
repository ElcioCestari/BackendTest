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
