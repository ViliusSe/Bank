using Dapper;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Models.Dtos.Responses;
using System.Data.Common;
using Domain.Models.Dtos.User;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IDbConnection _connection;

    public UserRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<Response<UserResponse, UserEntity>> Create(UserEntity entity)
    {
        var queryArguments = new
        {
            name = entity.Name,
            address = entity.Address,
            createdAt = entity.CreatedAt
        };
        var result = await _connection.QueryFirstOrDefaultAsync<UserEntity>(
            "INSERT INTO users (name, address, createdat) VALUES (@name, @address, @createdAt) RETURNING *;", queryArguments);

        if (result == null)
        {
            return new Response<UserResponse, UserEntity>()
            {
                IsSuccess = false,
                ErrorMessage = "Can't create user, try one more time",
            };
        }
        else
        {
            return new Response<UserResponse, UserEntity>()
            {
                IsSuccess = true,
                ErrorMessage = null,
                Data = new UserResponse() { Name = result.Name, Address = result.Address },
                Entity = result
            };
        }
    }

    public async Task<IEnumerable<UserEntity>> GetAll()
    {
        return await _connection.QueryAsync<UserEntity>("SELECT * FROM users WHERE deletedat IS NULL");
    }

    public async Task<UserEntity> GetById(int id)
    {
        return  await _connection.QueryFirstOrDefaultAsync<UserEntity>("SELECT * FROM users WHERE deletedat IS NULL AND id = @id", id);
    }

    public async Task<bool> Find(CreateUser dto)
    {
        string query = "SELECT * FROM users WHERE name = @Name AND address = @Address";
        var result = await _connection.QueryFirstOrDefaultAsync<int?>(query, new { Name = dto.Name, Address = dto.Address });

        return result != null;
    }

    public async Task<bool> Find(int userId)
    {
        var result = await _connection.QueryFirstOrDefaultAsync<int?>($"SELECT * FROM users WHERE id = {userId}");
        
        return result != null;
    }



}