using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions.BadRequest
{
    public class BasketBadRequestException() :BadRequestException("Invalid Opearion   create or update")
    {
    }
}
