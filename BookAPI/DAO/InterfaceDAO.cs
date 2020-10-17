using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookAPI.DAO
{
    public interface InterfaceDAO
    {
        List<Object> selectAll();
        Object select(int id);

    }
}
