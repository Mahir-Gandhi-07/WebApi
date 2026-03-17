using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using DTOs_AutoMapper.Models;
using DTOs_AutoMapper.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using DTOs_AutoMapper.DTOs.UserDTO;

namespace DTOs_AutoMapper.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        //[HttpPost("CreateUser")]
        //public IActionResult Register([FromBody] Usercreate request)
        //{
        //    var user = new User
        //    {
        //        UserName = request.Username,
        //        Role = request.Role,
        //        PasswordHash = request.Password
        //    };
        //    _userService.Register(user,request.Password);
        //    return Ok();
        //}

        //[HttpPost("login")]
        //public IActionResult Login([FromBody] LoginRequest u)
        //{
        //    var user = _userService.Authenticate(u.Username, u.Password);
        //    if (user == null) {
        //        return Unauthorized();

        //    }
        //    var token = _jwtService.GenerateToken(user);
        //    return Ok(new{token});
        //}

        //[Authorize(Roles = "admin")]
        //[HttpGet("{id}")]
        //public IActionResult GetUserById([FromRoute] int id)
        //{
        //    var user = _userService.GetUserById(id);
        //    if(user == null)
        //    {
        //        return NotFound("User not found");
        //    }
        //    return Ok(user);
        //}

        //[Authorize(Roles = "admin")]
        //[HttpGet]
        //public IActionResult GellAllUsers() { 
        //    var users = _userService.GetAllUsers();
        //    return Ok(users);
        //}

       

        [Authorize(Roles = "admin")]
        [HttpGet("allusers")]
        public async Task<IActionResult> getalluser()
        {
            var user = await _userService.GetAllUsers();
            return Ok(user);
        }


        [Authorize(Roles = "admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> getuser([FromRoute] int id) { 
            var user = await _userService.GetUserById(id);
            return Ok(user);
        }


        [Authorize(Roles = "admin")]
        [HttpPut("updateuser/{id}")]
        public async Task<IActionResult> updateuser([FromRoute] int id,[FromBody] Userupdate dto)
        {
            var user = await  _userService.updateuser(id,dto);
            return Ok("User Updated");
        }


        [Authorize(Roles = "admin")]
        [HttpDelete("deleteuser/{id}")]
        public async Task<IActionResult> deleteuser([FromRoute] int id)
        {
            await _userService.deleteuser(id);
            return Ok("User Deleted");
        }
    }
}
