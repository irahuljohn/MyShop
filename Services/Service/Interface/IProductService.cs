using MyShop.Models.DTO;

namespace MyShop.Services.Service.Interface
{
    public interface IProductService
    {
        Task<string> AddProduct(ProductModel model, string userId);
        Task<string> UpdateProduct(ProductModel model, string userId);
        Task<List<ProductModel>> GetAllProducts();
    }
}
