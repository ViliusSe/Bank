using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class TransactionEntity : BaseEntity
    {
        public int DebitorAccount { get; set; }
        public int CreditorAccount { get; set; }
        public decimal Amount {  get; set; } 
        public DateTime Date { get; set; }
    }
}
