using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Models.Entities
{
    public class OrderModel : BaseEntity
    {
        public int Qty { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public float TotalAmount { get; set; }
        public string? Status { get; set; }
        public string? Address { get; set; }
        public string? PaymentMethod { get; set; }

    }
}
