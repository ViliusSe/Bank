using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class WrongAccountTypeException : Exception
    {
        public WrongAccountTypeException() : base("No such type of accaunt can be created.") { }
    }
}
