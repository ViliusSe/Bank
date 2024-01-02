using Domain.Models.Dtos.Responses;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Models.Dtos.Account;
using Domain.Exceptions;
using Domain.UseCases;

namespace Application.Services
{
    public class AccountService
    {
        private readonly IAccountRepository _repository;
        private readonly IUserRepository _userRepository;

        public AccountService(IAccountRepository repository, IUserRepository userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }

        public async Task<Response<AccountResponse, AccountEntity>> Create(CreateAccount dto)
        {
            if (dto.Type != "Saving" && dto.Type != "Default")
                return new Response<AccountResponse, AccountEntity>
                {
                    IsSuccess = false,
                    ErrorMessage = "Wrong account type indicated. Should be Saving or Default."
                };

            var userExists = await _userRepository.Find(dto.UserId);
            if (!userExists)
                return new Response<AccountResponse, AccountEntity>
                {
                    IsSuccess = false,
                    ErrorMessage = $"No user with id {dto.UserId} found, please create user first."
                };

            var accountExists = await _repository.Find(dto);
            if (accountExists)
                return new Response<AccountResponse, AccountEntity>
                {
                    IsSuccess = false,
                    ErrorMessage = $"User already has {dto.Type} account."
                };

            string Iban = "";
            while (Iban == "")
            {
                Iban = IbanGenerator.GenerateIban();
                var ibanDublicate = await _repository.Find(Iban);
                if (ibanDublicate)
                    Iban = "";
            };

            var result = await _repository.Create(new AccountEntity
            {
                IBAN = Iban,
                Type = dto.Type,
                UserId = dto.UserId,
                Balance = 0m,
                CreatedAt = DateTime.UtcNow
            });

            return result;
        }
    }
}
