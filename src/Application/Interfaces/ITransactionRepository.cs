using Domain.Models.Dtos.Responses;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Dtos.Transaction;

namespace Application.Interfaces
{
    public interface ITransactionRepository
    {
        Task<Response<TransactionResponse, TransactionEntity>> Transfer(CreateTransaction dto);
        Task<Response<TransactionResponse, TransactionEntity>> TopUp(CreateTransaction dto);
        Task<IEnumerable<TransactionHistoryResponse>> GetHistoryByUserid(HistoryTransactions dto);
    }
}
