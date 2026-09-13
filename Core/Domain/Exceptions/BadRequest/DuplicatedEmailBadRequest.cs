using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions.BadRequest
{
    public class DuplicatedEmailBadRequest(string email): BadRequestException($"the email {email} is already exist")
    {
    }
}
