using DTOs_AutoMapper.DTOs.UserDTO;
using Microsoft.AspNetCore.Mvc;
using DTOs_AutoMapper.Services.AuthService;
using DTOs_AutoMapper.Services.JwtService;

namespace DTOs_AutoMapper.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _authservice;
        private readonly IJwtService _jwtService;
        public AuthController(IAuthService authService, IJwtService jwtService)
        {
            _authservice = authService;
            _jwtService = jwtService;
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] Usercreate dto)
        {
            //var user = _mapper.Map<User>(dto);
            await _authservice.Register(dto);
            return Ok("User Created");
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest u)
        {
            var user = await _authservice.Authenticate(u.Username, u.Password);
            if (user == null)
            {
                return Unauthorized();
            }
            var token = _jwtService.GenerateToken(user);
            return Ok(new
            {
                token,
                Message = "Login Successful"
            });
        }
    }
}
