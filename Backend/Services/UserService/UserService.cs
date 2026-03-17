using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using DTOs_AutoMapper.Data;
using DTOs_AutoMapper.Models;
using AutoMapper;
using DTOs_AutoMapper.DTOs.UserDTO;
using Microsoft.EntityFrameworkCore;

namespace DTOs_AutoMapper.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IMapper _mapper;

        public UserService(ApplicationDbContext context, IPasswordHasher<User> passwordHasher, IMapper mapper)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
        }

      

        public async Task<Userread?> GetUserById(int id)
        {
            var user = await _context.Users.FindAsync(id);
            return _mapper.Map<Userread>(user);
        }

        public async Task<List<Userread>> GetAllUsers()
        {
            var users = await _context.Users.ToListAsync();
            return _mapper.Map<List<Userread>>(users);
        }

        public async Task<Userread?> updateuser(int id, Userupdate dto)
        {
            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser == null)
            {
                return null;
            }

            _mapper.Map(dto, existingUser);

            if (!string.IsNullOrEmpty(dto.PasswordHash))
            {
                existingUser.PasswordHash =
                    _passwordHasher.HashPassword(existingUser, dto.PasswordHash);
            }

            await _context.SaveChangesAsync();

            return _mapper.Map<Userread>(existingUser);
        }

        public async Task deleteuser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
