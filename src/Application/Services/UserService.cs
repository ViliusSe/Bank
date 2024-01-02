using Application.Interfaces;
using Domain.Exceptions;
using Domain.Models;
using Domain.Models.Dtos.Responses;
using Domain.Models.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response<UserResponse, UserEntity>> Create(CreateUser dto)
        {
            var userExists = await _repository.Find(dto);
            if (userExists)
                return new Response<UserResponse, UserEntity>
                {
                    IsSuccess = false,
                    ErrorMessage = "User already exists."
                };

            var result = await _repository.Create(new UserEntity()
            {
                Name = dto.Name,
                Address = dto.Address,
                CreatedAt = DateTime.UtcNow
            }
            );

            return result;
        }

        public async Task<IEnumerable<UserEntity>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<UserEntity> GetById(int id)
        {
            return await _repository.GetById(id);
        }
    }
}
