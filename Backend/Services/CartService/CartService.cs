using AutoMapper;
using DTOs_AutoMapper.Data;
using DTOs_AutoMapper.DTOs.CartDTO;
using DTOs_AutoMapper.Models;
using Microsoft.EntityFrameworkCore;

namespace DTOs_AutoMapper.Services.CartService
{
    public class CartService : ICartService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CartService(ApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Cartread>?> getcartbyid(int userid)
        {
            var cart = await _context.Carts
                      .Where(c => c.UserId == userid)
                      .Include(c => c.products)
                      .Include(c => c.users)
                      .ToListAsync();
            return _mapper.Map<List<Cartread>>(cart);
        }


        public async Task Addcart(Cartcreate dto,int userid)
        {
            var existingcart = await _context.Carts.FirstOrDefaultAsync(c=>c.UserId == userid && c.ProductId == dto.ProductId);
            if (existingcart != null) {
                existingcart.quantity += dto.quantity;
            }
            else
            {
                var cart = _mapper.Map<Cart>(dto);
                cart.UserId = userid;
                _context.Carts.Add(cart);
            }

            await _context.SaveChangesAsync();
        }

   

        public async Task<Cartread> Updatecart(int cartId, Cartupdate dto, int userId)
        {
            var existingcart = await _context.Carts.FirstOrDefaultAsync(c => c.CartId == cartId);
            if (existingcart == null || existingcart.UserId != userId)
            {
                return null;
            }

            _mapper.Map(dto,existingcart);
            await _context.SaveChangesAsync();

            return _mapper.Map<Cartread>(existingcart);
        }
        public async Task Deletecart(int cartId, int userId)
        {
            var cart = await _context.Carts.FirstOrDefaultAsync(c => c.CartId == cartId);
            if (cart ==null)
                throw new Exception("Cart not found");

            if (cart.UserId != userId)
                throw new UnauthorizedAccessException("You cannot delete this cart");

            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();
        }
    }
}
