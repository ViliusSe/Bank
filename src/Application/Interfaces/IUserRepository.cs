using Domain.Models;
using Domain.Models.Dtos.Responses;
using Domain.Models.Dtos.User;


namespace Application.Interfaces
{
    public interface IUserRepository
    {
        Task<Response<UserResponse, UserEntity>> Create(UserEntity entity);
        Task<IEnumerable<UserEntity>> GetAll();
        Task<UserEntity> GetById(int id);
        Task<bool> Find(CreateUser dto);
        Task<bool> Find(int userId);
    }
}