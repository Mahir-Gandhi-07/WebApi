using DTOs_AutoMapper.Models;
using DTOs_AutoMapper.Data;
using AutoMapper;
using DTOs_AutoMapper.DTOs.ProductDTO;
using Microsoft.EntityFrameworkCore;

namespace DTOs_AutoMapper.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Productread>> GetAllProducts(int page, int pageSize)
        {
            var products = await _context.Products
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return _mapper.Map<List<Productread>>(products);
        }

        public async Task<List<Productread>> GetAllProducts()
        {
            var products = await _context.Products.ToListAsync();
            return _mapper.Map<List<Productread>>(products);
        }


        public async Task<Productread?> GetProductById(int id)
        {
            var product =await _context.Products.FindAsync(id);
            return _mapper.Map<Productread>(product);
        }

        public async Task Add(Productcreate dto)
        {
            var product =  _mapper.Map<Product>(dto);
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task<Productread> Update(int id, Productupdate dto)
        {
            var existingproduct =await _context.Products.FindAsync(id);

            if (existingproduct == null)
            {
                return null;
            }

            _mapper.Map(dto, existingproduct);
            await _context.SaveChangesAsync();


            return _mapper.Map<Productread>(existingproduct);
        }

        public async Task Delete(int id)
        {
            var product =await _context.Products.FirstOrDefaultAsync(x => x.ProductId == id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}
