using Domain.Models.Dtos.Responses;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Application.Interfaces;
using Domain.Models.Dtos.User;
using Domain.Models.Dtos.Account;

namespace Infrastructure.Repositories { }


public class AccountRepository : IAccountRepository
{
    private readonly IDbConnection _connection;

    public AccountRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<Response<AccountResponse, AccountEntity>> Create(AccountEntity entity)
    {
        var queryArguments = new
        {
            iban = entity.IBAN,
            type = entity.Type,
            balance = entity.Balance,
            userId = entity.UserId
        };
        var result = await _connection.QueryFirstOrDefaultAsync<AccountEntity>(
        "INSERT INTO accounts (iban, type, balance, userid) VALUES (@iban, @type, @balance, @userId) RETURNING *;", queryArguments);

        if (result == null)
        {
            return new Response<AccountResponse, AccountEntity>()
            {
                IsSuccess = false,
                ErrorMessage = "Can't create account, try one more time",
            };
        }
        else
        {
            return new Response<AccountResponse, AccountEntity>()
            {
                IsSuccess = true,
                ErrorMessage = null,
                Data = new AccountResponse() { IBAN = result.IBAN, Type = result.Type, Balance = result.Balance },
                Entity = result
            };
        }
    }

    public async Task<bool> Find(CreateAccount dto)
    {
        string query = "SELECT * FROM accounts WHERE userid = @UserId AND type = @Type";
        var result = await _connection.QueryFirstOrDefaultAsync<int?>(query, new { UserId = dto.UserId, Type = dto.Type });

        return result != null;
    }

    public async Task<bool> Find(string IBAN)
    {
        string query = "SELECT * FROM accounts WHERE iban = @IBAN";
        var result = await _connection.QueryFirstOrDefaultAsync<int?>(query, new { IBAN = IBAN });

        return result != null;
    }

    public async Task<AccountEntity?> Find(int id)
    {
        string query = "SELECT * FROM accounts WHERE id = @id";
        var result = await _connection.QueryFirstOrDefaultAsync<AccountEntity>(query, new { id = id });

        return result;
    }
}


