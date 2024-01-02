using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class UserExistsException : Exception
    {
        public UserExistsException() : base("This user is already exists.") { }
    }
}
