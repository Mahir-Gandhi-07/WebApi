using DTOs_AutoMapper.DTOs.CartDTO;

namespace DTOs_AutoMapper.Services.CartService
{
    public interface ICartService
    {
        //public List<Cartread> getcarts();
        public Task<List<Cartread>?> getcartbyid(int userid);
        public Task Addcart(Cartcreate dto,int userid);
        public Task<Cartread> Updatecart(int cartId, Cartupdate dto, int userId);
        public Task Deletecart(int cartId, int userId);
    }
}
