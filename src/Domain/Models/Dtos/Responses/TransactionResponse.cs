using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Dtos.Responses
{
    public class TransactionResponse
    {
        public int DebitorAccount { get; set; }  //DebitAccount during top up will be represented by same id as a CreditAccount
        public int CreditorAccount { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
