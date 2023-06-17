using MyShop.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Models.DTO
{
    public class CategoryModel
    {
        public int ID { get; set; }
        public string? Name { get; set; }
    }
    public class CategoryModelList
    {
        public List<ProductCategory> List { get; set; }
        public int TotalRecords { get; set; }
    }

}
