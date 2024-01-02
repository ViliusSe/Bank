using Domain.Models.Dtos.Responses;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Reflection;
using Application.Interfaces;
using Domain.Models.Dtos.Transaction;

namespace Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly IDbConnection _connection;

        public TransactionRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<Response<TransactionResponse, TransactionEntity>> Transfer(CreateTransaction dto)
        {
            var queryArguments = new
            {
                DebitorAccountId = dto.DebitorAccounId,
                CreditorAccountId = dto.CreditorAccountId,
                Amount = dto.Amount,
                Date = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow
            };
            string query = "BEGIN;" +
                            " INSERT INTO transactions (debitorAccount, creditorAccount, amount,  date, createdat)" +
                            " VALUES (@DebitorAccountId, @CreditorAccountId, @Amount, @Date, @CreatedAt)" +
                            " RETURNING *;" +
                            " UPDATE accounts SET balance =  CASE" +
                            " WHEN id = @DebitorAccountId THEN balance - (@Amount + 1)::money" +
                            " WHEN id = @CreditorAccountId THEN balance + @Amount::money" +
                            " ELSE balance" +
                            " END" +
                            " WHERE id IN(@DebitorAccountId, @CreditorAccountId); " +
                            "COMMIT;";

            var result = await _connection.QueryFirstOrDefaultAsync<TransactionEntity>(query, queryArguments);

            if (result == null)
            {
                return new Response<TransactionResponse, TransactionEntity>()
                {
                    IsSuccess = false,
                    ErrorMessage = "Can't make transaction, try one more time",
                };
            }
            else
            {
                return new Response<TransactionResponse, TransactionEntity>()
                {
                    IsSuccess = true,
                    ErrorMessage = null,
                    Data = new TransactionResponse()
                    {
                        DebitorAccount = result.DebitorAccount,
                        CreditorAccount = result.CreditorAccount,
                        Amount = result.Amount,
                        Date = result.Date,
                    }
                };
            }
        }

        public async Task<Response<TransactionResponse, TransactionEntity>> TopUp(CreateTransaction dto)
        {
            var queryArguments = new
            {
                DebitorAccountId = dto.DebitorAccounId,
                CreditorAccountId = dto.CreditorAccountId,
                Amount = dto.Amount,
                Date = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow
            };
            string query = @"BEGIN;
                                INSERT INTO transactions (debitorAccount, creditorAccount, amount,  date, createdat)
                                    VALUES (@DebitorAccountId, @CreditorAccountId, @Amount, @Date, @CreatedAt)
                                    RETURNING *;
                                UPDATE accounts SET balance =  balance + @Amount::money WHERE id = @CreditorAccountId;
                            COMMIT;";

            var result = await _connection.QueryFirstOrDefaultAsync<TransactionEntity>(query, queryArguments);

            if (result == null)
            {
                return new Response<TransactionResponse, TransactionEntity>()
                {
                    IsSuccess = false,
                    ErrorMessage = "Can't make transaction, try one more time",
                };
            }
            else
            {
                return new Response<TransactionResponse, TransactionEntity>()
                {
                    IsSuccess = true,
                    ErrorMessage = null,
                    Data = new TransactionResponse()
                    {
                        DebitorAccount = result.DebitorAccount,
                        CreditorAccount = result.CreditorAccount,
                        Amount = result.Amount,
                        Date = result.Date,
                    }
                };
            }
        }

        public async Task<IEnumerable<UserEntity>> GetAll()
        {
            return await _connection.QueryAsync<UserEntity>("SELECT * FROM users WHERE deletedat IS NULL");
        }

        //public async Task<Response<IEnumerable<TransactionResponse>,TransactionEntity>> GetByUserid(HistoryTransactions dto)
        public async Task<IEnumerable<TransactionHistoryResponse>> GetHistoryByUserid(HistoryTransactions dto)
        {
            var queryArguments = new
            {
                UserId = dto.UserId,
                OrderBy = dto.OrderBy,
                Direction = dto.Direction
            };
            string query = @"SELECT
                                t.id AS transaction_id,
                                da.IBAN AS debitor_IBAN,
                                ca.IBAN AS creditor_IBAN,
                                t.amount AS transfer_amount,
                                t.date AS transaction_date
                            FROM
                                transactions t
                            JOIN
                                accounts da ON t.debitorAccount = da.id
                            JOIN
                                accounts ca ON t.creditorAccount = ca.id
                            WHERE
                                da.userId = @UserId
                                OR ca.userId = @UserId
                            ORDER BY
                                @OrderBy @Direction;";

            var result = await _connection.QueryAsync<TransactionHistoryResponse>(query, queryArguments);
            return result;
        }
    }
}
