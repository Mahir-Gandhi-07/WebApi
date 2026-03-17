namespace DTOs_AutoMapper.DTOs.ProductDTO
{
    public class Productcreate
    {
        public string? ProductImg { get; set; }
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public int price { get; set; }
        public int quantity { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;

    }
}
