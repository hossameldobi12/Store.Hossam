using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions.NotFound
{
    public class OrderNotFoundException(Guid id) :NotFoundException($"Order With ID {id} Not Found")
    {
    }
}
