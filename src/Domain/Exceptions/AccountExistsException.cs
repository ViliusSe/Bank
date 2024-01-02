using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class AccountExistsException : Exception
    {
        public AccountExistsException() : base("This account type allready exists.") { }
    }
}
