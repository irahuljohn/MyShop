using MyShop.Models.DTO;
using MyShop.Models.Entities;

namespace MyShop.Services.Service.Interface
{
    public interface ICategoryService
    {
        Task<string> InsertCategory(CategoryModel category, string userId);
        Task<CategoryModelList> GetCategory(int PageNo, int PageSize, string sortColumn = "", string sortColumnDirection = "", string Search = "");
        Task<List<ProductCategory>> GetCategory();
        Task<string> EditCategory(CategoryModel category, string userId);
        Task<string> DeleteCategory(int categoryId);
    }
}
