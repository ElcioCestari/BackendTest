using BookAPI;
using BookAPI.DAO;
using NUnit.Framework;
using System.Collections.Generic;

namespace BookAPINUnitTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void testConstructorIsNotNull()
        {
            BookDAO booKDAO = new BookDAO();

            Assert.IsNotNull(booKDAO);
        }

        [Test]
        public void testSetBooksJSON_booksJSONIsNotNull()
        {
            BookDAO booKDAO = new BookDAO();

            Assert.IsNotNull(booKDAO);
        }

        [Test]
        public void testSetBooksJSON_throwsException()
        {
            try
            {
                BookDAO booKDAO = new BookDAO();
            }
            catch (System.Exception)
            {
                return;
            }

            Assert.Fail();
        }


        public TestContext TestContext { get; set; }

        [Test]
           public void testSelectAllIsNotNull()
        {
            TestContext.WriteLine("inicializando book");
            BookDAO book = new BookDAO();

            TestContext.WriteLine("executando o metodo a ser testado");
            List<Book> books = book.selectAll();

            TestContext.WriteLine("verificadon se nao é null");
            Assert.IsNotNull(books);

        }

        [Test]
        public void testSearchById_IdThatNotExists()
        {
            BookDAO bookDAO = new BookDAO();

            TestContext.WriteLine("teste com id 0 que nao existe");
            Book book = bookDAO.searchById(0);// id 0 nao existe.
            Assert.IsNull(book);

        }

        [Test]
        public void testSearchById_IdThatExists()
        {
            BookDAO bookDAO = new BookDAO();
            int id = 1;

            TestContext.WriteLine("teste com id: " + id + " existe");

            Book book = bookDAO.searchById(1);
            Assert.IsNotNull(book);

            TestContext.WriteLine("teste para garantir que os ids são iguais");

            Assert.AreEqual(book.id, id);

        }

        [Test]
        public void testSearchByAuthName_WithInvalideNameFormat()
        {
            BookDAO bookDAO = new BookDAO();

            string invalidParameter = "";

            TestContext.WriteLine("parametro: " + invalidParameter);
            List<Book> books = bookDAO.searchByAuthName(invalidParameter);

            Assert.IsEmpty(books);

        }
        [Test]
        public void testSearchByAuthName()
        {
            BookDAO bookDAO = new BookDAO();

            string authName = "J. R. R. Tolkien";

            TestContext.WriteLine("parametro: " + authName);
            List<Book> books = bookDAO.searchByAuthName(authName);

            foreach (var booksByAuth in books)
            {
                Assert.AreEqual(booksByAuth.specifications.Author, authName);
            }

        }

    }
}