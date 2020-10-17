using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace BookAPI.DAO
{
    public class BookDAO : InterfaceDAO
    {
        object InterfaceDAO.select(int id)
        {
            throw new NotImplementedException();
        }

        //TODO esse metodo ainda não esta totalmente funcional
        //desejo implementar uma classe DAO para melhorar a manutenibilidade
        //entretanto estou com dificuldade nesse momento.
        //portanto vou implementar as funcionalidades e, caso haja tempo, 
        //irei focar nessas funcionalidades.
        List<Object> InterfaceDAO.selectAll()
        {
            String txt = System.IO.File
                .ReadAllText(System.AppDomain.CurrentDomain.BaseDirectory.ToString() + @"\Data\books.JSON");

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

            return null;
        }
    }
}
