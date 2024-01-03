using Domain.Models;
using Domain.Models.Dtos.Account;
using Domain.Models.Dtos.Responses;


namespace Application.Interfaces
{
    public interface IAccountRepository
    {
        Task<Response<AccountResponse, AccountEntity>> Create(AccountEntity entity);
        Task<IEnumerable<AccountEntity>> GetAll();
        Task<bool> Find(CreateAccount dto);
        Task<bool> Find(string iban);
        Task<AccountEntity?> Find(int id);
        Task<IEnumerable<AccountEntity?>> FindAll(int id);
        
    }
}
