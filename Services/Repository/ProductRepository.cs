using MyShop.Models.Entities;
using MyShop.Services.Context;
using MyShop.Services.Repository.Interface;

namespace MyShop.Services.Repository
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(MyShopContext dbContext)
          : base(dbContext)
        {
        }
    }
}
