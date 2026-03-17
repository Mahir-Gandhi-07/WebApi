using DTOs_AutoMapper.Models;

namespace DTOs_AutoMapper.DTOs.CartDTO
{
    public class Cartread
    {
        public string? UserName { get; set; }
        public string? ProductImg { get; set; }
        public string? ProductName { get; set; }
        public int price { get; set; }
        public int quantity { get; set; }

    }
}
