using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyShop.Models.DTO;
using MyShop.Services.Service.Interface;
using System.Collections.Generic;
using System.Security.Claims;

namespace MyShop.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        #region Const
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICategoryService _Category;
        private readonly IProductService _product;

        public ProductController(ICategoryService category, IProductService product,
           IHttpContextAccessor httpContextAccessor)
        {
            _Category = category;
            _product = product;
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion

        #region Category
        public IActionResult Category()
        {
            return View();
        }
        
        [HttpPost]
        /// <summary>
        /// Insert Product Category.. 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<IActionResult> AddCategory(CategoryModel model)
        {
            var result = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(model.Name))
                {
                    return BadRequest("Category name cannot be null.");
                }
                var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (model.ID > 0)
                    result = await _Category.EditCategory(model, userId);

                result = await _Category.InsertCategory(model, userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// get category list.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GetCategory()
        {
            try
            {
                // This values get from Datatables 
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][data]"].FirstOrDefault();
                if (sortColumn == "")
                {
                    sortColumn = "Category";
                }
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 10;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int pageno = skip == 0 ? 1 : (skip / pageSize) + 1;

                var result = await _Category.GetCategory(pageno, pageSize, sortColumn, sortColumnDirection, searchValue);
                return Ok(new
                {
                    draw = Convert.ToInt32(draw),
                    recordsFiltered = result.TotalRecords,
                    recordsTotal = result.TotalRecords,
                    data = result.List
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetCategorys()
        {
            try 
            {
                var result = await _Category.GetCategory();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// For delete the category..
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                var result = await _Category.DeleteCategory(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        #endregion

        public IActionResult Product()
        {
            return View();
        }

        [HttpPost]
        /// <summary>
        /// Add new and Update the Product.. 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<IActionResult> AddProduct(ProductModel model)
        {
            var result = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(model.Name))
                {
                    return BadRequest("Category name cannot be null.");
                }
                var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (model.Id > 0)
                    result = await _product.UpdateProduct(model, userId);

                result = await _product.AddProduct(model, userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// get Product list.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var result = await _product.GetAllProducts();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
