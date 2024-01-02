﻿using Application.Interfaces;
using Domain.Models.Dtos.Account;
using Domain.Models.Dtos.Responses;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Dtos.Transaction;
using System.Xml;

namespace Application.Services
{
    public class TransactionService
    {
        private readonly ITransactionRepository _repository;
        private readonly IAccountRepository _accountRepository;
        private readonly IUserRepository _userRepository;

        public TransactionService(ITransactionRepository repository, IAccountRepository accountRepository, IUserRepository userRepository)
        {
            _repository = repository;
            _accountRepository = accountRepository;
            _userRepository = userRepository;
        }

        public async Task<Response<TransactionResponse, TransactionEntity>> Create(CreateTransaction dto)
        {
            if (dto.Amount <= 0)
                return new Response<TransactionResponse, TransactionEntity>
                {
                    IsSuccess = false,
                    ErrorMessage = "Entered ammount is not valid."
                };
            
            var checkDebitor = await _accountRepository.Find(dto.DebitorAccounId);
            if (checkDebitor is null)
                return new Response<TransactionResponse, TransactionEntity>
                {
                    IsSuccess = false,
                    ErrorMessage = "Can't transfer money from this account as it doesn't exist."
                };

            var checkCreditor = await _accountRepository.Find(dto.CreditorAccountId);
            if (checkCreditor is null)
                return new Response<TransactionResponse, TransactionEntity>
                {
                    IsSuccess = false,
                    ErrorMessage = "Can't transfer money to this account as it doesn't exist."
                };

            if (checkDebitor.Balance < (dto.Amount + 1m))
                return new Response<TransactionResponse, TransactionEntity>
                {
                    IsSuccess = false,
                    ErrorMessage = "Not enough money in account to make transfer."
                };

            var debitor = _accountRepository.Find(dto.DebitorAccounId);
            var creditor = _accountRepository.Find(dto.CreditorAccountId);

            Response<TransactionResponse, TransactionEntity> result = new Response <TransactionResponse,TransactionEntity>();
            return result = await _repository.Transfer(dto);
        }

        public async Task<Response<TransactionResponse, TransactionEntity>> TopUp(CreateTransaction dto)
        {
            if (dto.Amount <= 0)
                return new Response<TransactionResponse, TransactionEntity>
                {
                    IsSuccess = false,
                    ErrorMessage = "Entered ammount is not valid."
                };

            var checkCreditor = await _accountRepository.Find(dto.DebitorAccounId);
            if (checkCreditor is null)
                return new Response<TransactionResponse, TransactionEntity>
                {
                    IsSuccess = false,
                    ErrorMessage = "Can't transfer money to this account as it doesn't exist."
                };
           
            var creditor = _accountRepository.Find(dto.CreditorAccountId);
            var debitor = creditor;

            Response <TransactionResponse, TransactionEntity> result = new Response<TransactionResponse, TransactionEntity>();
            return result = await _repository.TopUp(dto);
        }

        public async Task<Response<IEnumerable<TransactionHistoryResponse>, HistoryTransactions>> GetHistoryByUserid(HistoryTransactions dto)
        {
            if (dto.OrderBy != "da" || dto.OrderBy != "ca" || dto.OrderBy != "amount" || dto.OrderBy != "date")
                    return new Response<IEnumerable<TransactionHistoryResponse>, HistoryTransactions>
                    {
                        IsSuccess = false,
                        ErrorMessage = "Incorect ordering specified. Should be da, ca, amount or date."
                    };


            if (dto.Direction != "ASC" || dto.Direction != "DESC")
            {
                return new Response<IEnumerable<TransactionHistoryResponse>, HistoryTransactions>
                {
                    IsSuccess = false,
                    ErrorMessage = "Incorrect sorting method specified. Should be ASC or DSC."
                };
            }

            var checkUser = await _userRepository.Find(dto.UserId);
            if (!checkUser)
                return new Response<IEnumerable<TransactionHistoryResponse>, HistoryTransactions>
                {
                    IsSuccess = false,
                    ErrorMessage = $"User with id {dto.UserId} doesn't exist."
                };

        var result = await _repository.GetHistoryByUserid(dto);
            return new Response<IEnumerable<TransactionHistoryResponse>, HistoryTransactions>
            {
                IsSuccess = true,
                ErrorMessage = null,
                Data = result
            };
        }
    }
}
