using AutoMapper;
using DTOs_AutoMapper.Data;
using DTOs_AutoMapper.DTOs.UserDTO;
using DTOs_AutoMapper.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DTOs_AutoMapper.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IMapper _mapper;

        public AuthService(ApplicationDbContext context, IPasswordHasher<User> passwordHasher, IMapper mapper)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
        }

        public async Task Register(Usercreate dto)
        {
            var user = _mapper.Map<User>(dto);

            user.PasswordHash = _passwordHasher.HashPassword(user, dto.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<Userread?> Authenticate(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
            if (user == null)
            {
                return null;
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            if (result == PasswordVerificationResult.Failed)
            {
                return null;
            }
            return _mapper.Map<Userread>(user);
        }
    }
}
