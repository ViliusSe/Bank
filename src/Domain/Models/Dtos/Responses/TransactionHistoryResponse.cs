using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Dtos.Responses
{
    public class TransactionHistoryResponse
    {
        public string Debbitor_IBAN {  get; set; }
        public string Creditor_IBAN { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date {  get; set; }
    }
}
