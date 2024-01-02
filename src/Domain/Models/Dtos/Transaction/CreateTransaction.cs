using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Dtos.Transaction
{
    public class CreateTransaction
    {
        public int DebitorAccounId { get; set; }
        public int CreditorAccountId { get; set; }
        public decimal Amount {  get; set; }
    }
}
