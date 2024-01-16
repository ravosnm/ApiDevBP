using ApiDevBP.Models;

namespace ApiDevBP.Services.Interfaces
{
    public interface IUserService
    {
        Task<ResponseResultModel> CreateUser(UserModel user);
        Task<ResponseResultModel> GetUsers();
        Task<ResponseResultModel> UpdateUser(UserModel user);
        Task<ResponseResultModel> DeleteUser(UserModel user);
    }
}