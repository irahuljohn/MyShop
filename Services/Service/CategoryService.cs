using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyShop.Models.DTO;
using MyShop.Models.Entities;
using MyShop.Services.Context;
using MyShop.Services.Repository.Interface;
using MyShop.Services.Service.Interface;
using System.Security.Cryptography.X509Certificates;

namespace MyShop.Services.Service
{
    public class CategoryService : ICategoryService
    {
       private readonly MyShopContext _myShopContext;
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository, MyShopContext myShopContext)
        {
            _categoryRepository = categoryRepository;
            _myShopContext = myShopContext;
        }

        public async Task<string> InsertCategory(CategoryModel category, string userId)
        {
            try
            {
                var CategoryModel = new ProductCategory()
                {
                    Category = category.Name,
                    CreatedDate = DateTime.Now,
                    IsDeleted = false,
                    CreatedBy = userId,
                };
                var res = await _categoryRepository.CreateAsync(CategoryModel);

                if (res != null && res.Id > 0)
                    return "Sucess";
                else
                    return "Failed";
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<CategoryModelList> GetCategory(int PageNo, int PageSize, string sortColumn = "", 
            string sortColumnDirection = "", string Search = "")
        {
            try
            {
                //var res = await _categoryRepository.GetAllAsync(x => x.IsDeleted == false);
                //return res;
                IQueryable<ProductCategory> query = _myShopContext.ProductCategory.Where(x => x.IsDeleted == false)
                                                   .AsNoTracking();

                var size = await query.CountAsync();

                int skip = 0;
                if (PageNo == 1)
                    skip = 0;
                else
                    skip = (PageNo - 1) * PageSize;

                var items = await query
                .AsNoTracking()
                .Skip(skip)
                .Take(PageSize).ToListAsync();

                return new CategoryModelList()
                {
                    List = items,
                    TotalRecords = size
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<ProductCategory>> GetCategory()
        {
            try
            {
                var res = await _categoryRepository.GetAllAsync(x => x.IsDeleted == false);
                return res;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<string> EditCategory(CategoryModel category, string userId)
        {
            try
            {
                var objProject = await _categoryRepository.FirstOrDefaultAsync(x => x.Id == category.ID && x.IsDeleted == false);
                if (objProject == null) return "Not found";

                objProject.Category = category.Name;
                objProject.UpdatedDate = DateTime.Now;
                objProject.IsDeleted = false;
                objProject.UpdatedBy = userId;

                var res = await _categoryRepository.UpdateAsync(objProject);

                if (res != null && res.Id > 0)
                    return "Updated sucess";
                else
                    return "Failed";
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<string> DeleteCategory(int categoryId)
        {
            try
            {
                var objProject = await _categoryRepository.FirstOrDefaultAsync(x => x.Id == categoryId && x.IsDeleted == false);
                if (objProject == null) return "Not found";

                await _categoryRepository.DeleteAsync(categoryId);
                return "deleted";
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
