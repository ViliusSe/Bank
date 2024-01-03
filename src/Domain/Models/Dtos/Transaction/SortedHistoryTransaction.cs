using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Dtos.Transaction
{
    public class SortedHistoryTransaction
    {
        public string Type { get; set; } = "Transfer";
        public DateTime Date { get; set; }
        public string AccountIBAN { get; set; } = "";
        public decimal Amount {  get; set; }
        public int Transferfee { get; set; }
        
    }
}
