using MyShop.Models.Entities;
using MyShop.Services.Context;
using MyShop.Services.Repository.Interface;


namespace MyShop.Services.Repository
{
    public class CategoryRepository : BaseRepository<ProductCategory>, ICategoryRepository
    {
        public CategoryRepository(MyShopContext dbContext)
            : base(dbContext)
        {
        }

    }
}
