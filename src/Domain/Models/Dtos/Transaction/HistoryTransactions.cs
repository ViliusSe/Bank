using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Dtos.Transaction
{
    public class HistoryTransactions
    {
        public int UserId { get; set; }
        public string OrderBy { get; set; } = "da";
        public string Direction { get; set; } = "ASC";
    }
}
