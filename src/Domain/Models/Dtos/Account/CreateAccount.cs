using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Dtos.Account
{
    public class CreateAccount
    {
        public string Type { get; set; }
        public int UserId { get; set; }
    }
}
