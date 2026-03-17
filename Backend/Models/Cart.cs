
namespace DTOs_AutoMapper.Models
{
    public class Cart
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int quantity { get; set; }
        public User? users { get; set; }
        public Product? products { get; set; }
    }
}
