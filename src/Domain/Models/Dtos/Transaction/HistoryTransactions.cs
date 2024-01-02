using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Dtos.Transaction
{
    public class HistoryTransactions
    {
        public string Type { get; set; } //debbitorId creditorId
        public int AccountId { get; set; }
        public string OrderBy { get; set; } //debbitorId creditorId amount
        public string SortBy { get; set; }  // -||-
        public string Direction { get; set; } //DESC ASC
    }
}
