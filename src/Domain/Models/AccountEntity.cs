using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class AccountEntity : BaseEntity
    {
        public string IBAN { get; set; }
        public string Type { get; set; }
        public decimal Balance { get; set; }
        public int UserId { get; set; }
    }
}
