using DTOs_AutoMapper.DTOs.ProductDTO;
using DTOs_AutoMapper.Models;
namespace DTOs_AutoMapper.Services.ProductService
{
    public interface IProductService
    {
        public  Task<List<Productread>> GetAllProducts(int page, int pageSize);
        public  Task<List<Productread>> GetAllProducts();
        public  Task<Productread?> GetProductById(int id);
        public  Task Add(Productcreate dto);
        public  Task<Productread> Update(int id, Productupdate dto);
        public  Task Delete(int id);

    }
}
