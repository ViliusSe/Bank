using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Dtos.Responses
{
    public class TransactionHistoryResponse
    {
        public string DebbitorAccount {  get; set; }
        public string CreditorAccount { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
