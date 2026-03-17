using DTOs_AutoMapper.DTOs.UserDTO;
using DTOs_AutoMapper.Models;

namespace DTOs_AutoMapper.Services.UserService
{
    public interface IUserService
    {

        public Task<Userread?> GetUserById(int id);

        public Task<List<Userread>> GetAllUsers();

        public Task<Userread?> updateuser(int id, Userupdate dto);

        public Task deleteuser(int id);
    }
}
