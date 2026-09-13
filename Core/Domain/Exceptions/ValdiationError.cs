using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class ValdiationError(IEnumerable<string> errors) : Exception("Validation failed")
    {
        public IEnumerable<string> Errors { get; } = errors;
    }
}
