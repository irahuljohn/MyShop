using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Models.DTO
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public string? Category { get; set; }
        public float Price { get; set; }
        public string? ProductImage { get; set; }
        public int Quantity { get; set; }
    }
}
