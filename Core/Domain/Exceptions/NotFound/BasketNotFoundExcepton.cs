using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions.NotFound
{
    public class BasketNotFoundExcepton(string id ) :NotFoundException($"Basket With ID {id} Not Found")
    {
    }
}
