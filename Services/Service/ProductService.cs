using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using MyShop.Models.DTO;
using MyShop.Services.Context;
using MyShop.Services.Repository;
using MyShop.Services.Repository.Interface;
using MyShop.Services.Service.Interface;
using System.Diagnostics;

namespace MyShop.Services.Service
{
    public class ProductService : IProductService
    {
        private readonly MyShopContext _myShopContext;
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository, MyShopContext myShopContext)
        {
            _myShopContext = myShopContext;
            _productRepository = productRepository;
        }
        public async Task<string> AddProduct(ProductModel model, string userId)
        {
            try
            {
                var promodel = new MyShop.Models.Entities.Product()
                {
                    ProductName = model.Name,
                    ProductDescription = model.Description,
                    CategoryId = model.CategoryId,
                    Price = model.Price,
                    CreatedBy = userId,
                    CreatedDate = DateTime.Now,
                    IsDeleted = false,
                    ProductImage = model.ProductImage,
                    Quantity = model.Quantity
                };
                var res = await _productRepository.CreateAsync(promodel);

                if (res != null && res.Id > 0)
                    return "Sucess";
                else
                    return "Failed";
            }
            catch 
            { throw; }
        }

        public async Task<string> UpdateProduct(ProductModel model, string userId)
        {
            try
            {
                var objProduct = await _productRepository.FirstOrDefaultAsync(x => x.Id == model.Id && x.IsDeleted == false);
                if (objProduct == null) return "Not found";

                objProduct.UpdatedDate = DateTime.Now;
                objProduct.ProductName = model.Name;
                objProduct.ProductDescription = model.Description;
                objProduct.Price = model.Price;
                objProduct.CategoryId = model.CategoryId;

                var res = await _productRepository.UpdateAsync(objProduct);

                if (res != null && res.Id > 0)
                    return "Updated sucess";
                else
                    return "Failed";
            }
            catch { throw; }
        }

        public async Task<List<Models.DTO.ProductModel>> GetAllProducts()
        {
            try
            {
                //var ress = await _myShopContext.ProductCategory();
                var res = _myShopContext.ProductModel
                .Join(
                    _myShopContext.ProductCategory,
                    product => product.CategoryId,
                    category => category.Id,
                    (product, category) => new { product, category }
                            ).Where(x => x.product.IsDeleted == false).Select(x => new ProductModel()
                            {
                                Category = x.category.Category,
                                Name = x.product.ProductName, 
                                Description = x.product.ProductDescription,
                                Price = x.product.Price,
                                Quantity = x.product.Quantity
                            }).ToList();
    //.Join(
    //    dbContext.tbl_Title,
    //    combinedEntry => combinedEntry.entry.TID,
    //    title => title.TID,
    //    (combinedEntry, title) => new
    //    {
    //        UID = combinedEntry.entry.OwnerUID,
    //        TID = combinedEntry.entry.TID,
    //        EID = combinedEntry.entryPoint.EID,
    //        Title = title.Title
    //    }
    //)
    //.Where(fullEntry => fullEntry.UID == user.UID)
    //.Take(10);



                //var res = await _productRepository.GetAllAsync(x => x.IsDeleted == false);
                return res;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
