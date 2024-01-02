using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Dtos.Responses
{
    public class AccountResponse
    {
        public string IBAN { get; set; }
        public string Type { get; set; }
        public decimal Balance { get; set; }
    }
}
