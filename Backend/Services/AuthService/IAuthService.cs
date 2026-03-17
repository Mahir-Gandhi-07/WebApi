using DTOs_AutoMapper.DTOs.UserDTO;

namespace DTOs_AutoMapper.Services.AuthService
{
    public interface IAuthService
    {
        public Task Register(Usercreate dto);

        public Task<Userread?> Authenticate(string username, string password);

    }
}
