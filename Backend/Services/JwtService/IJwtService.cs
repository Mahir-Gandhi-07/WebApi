using DTOs_AutoMapper.DTOs.UserDTO;
using DTOs_AutoMapper.Models;

namespace DTOs_AutoMapper.Services.JwtService
{
    public interface IJwtService
    {
        string GenerateToken(Userread user);
    }
}
