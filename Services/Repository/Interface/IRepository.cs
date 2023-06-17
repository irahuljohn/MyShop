using MyShop.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Services.Repository.Interface
{
    public interface IRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// Get specific record by <paramref name="id"/>. 
        /// </summary>
        /// <param name="id">Record id.</param>
        /// <returns></returns>
        System.Threading.Tasks.Task<T> GetByIdAsync(int id);
        System.Threading.Tasks.Task<T> GetByIdAsync(int id, params string[] includes);
        System.Threading.Tasks.Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> filter = null, params string[] includes);

        /// <summary>
        /// Get all records.
        /// </summary>
        /// <returns></returns>
        System.Threading.Tasks.Task<List<T>> GetAllAsync();

        System.Threading.Tasks.Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, params string[] includes);

        /// <summary>
        /// Create record.
        /// </summary>
        /// <param name="model">Record that need to create.</param>
        /// <returns>Newly created record.</returns>
        System.Threading.Tasks.Task<T> CreateAsync(T model);

        /// <summary>
        /// Update existing record.
        /// </summary>
        /// <param name="id">Id of the record that need to update.</param>
        /// <param name="model">Record that need to update.</param>
        /// <returns>Updated record.</returns>
        System.Threading.Tasks.Task<T> UpdateAsync(T model);

        /// <summary>
        /// Delete specific record.
        /// </summary>
        /// <param name="id">Record id.</param>
        System.Threading.Tasks.Task DeleteAsync(int id);

        /// <summary>
        /// Check if record exist or not.
        /// </summary>
        /// <param name="id">Record id.</param>
        /// <returns><c>true</c> if record exist, else <c>false</c>.</returns>
        System.Threading.Tasks.Task<bool> ExistAsync(int id);
        /// <summary>
        ///  Check if record exist or not with filtering
        /// </summary>
        /// <param name="filter"></param>
        /// <returns><c>true</c> if record exist, else <c>false</c>.</returns>
        System.Threading.Tasks.Task<bool> ExistAsync(Expression<Func<T, bool>> filter);


    }
}
