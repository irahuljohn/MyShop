namespace MyShop.Models.Entities
{
    public class Product : BaseEntity
    {
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public string? ProductImage { get; set; }
        public int? CategoryId { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }

    }
}
