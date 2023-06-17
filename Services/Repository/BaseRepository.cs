using Microsoft.EntityFrameworkCore;
using MyShop.Models.Entities;
using MyShop.Services.Context;
using MyShop.Services.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Services.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        #region Properties
        private readonly MyShopContext _dbContext;

        #endregion

        #region Constr
        protected BaseRepository(MyShopContext dbContext)
        {
            this._dbContext = dbContext;
        }
        #endregion

        #region Methods
        public virtual async Task<T> CreateAsync(T model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("entity");
            }
            await _dbContext.AddAsync<T>(model);
            await _dbContext.SaveChangesAsync();

            return model;
        }
        public virtual async Task DeleteAsync(int id)
        {
            var model = await _dbContext.FindAsync<T>(id);
            if (model != null)
            {
                model.IsDeleted = true;
                _dbContext.Set<T>().Update(model);
                await _dbContext.SaveChangesAsync();
            }
        }
        public virtual async Task<List<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
        }
        public virtual async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, params string[] includes)
        {
            var query = _dbContext.Set<T>().AsNoTracking().AsQueryable();

            foreach (var include in includes)
                query = query.Include(include);

            if (filter != null)
                query = query.Where(filter);

           // return await query.ToArrayAsync().ConfigureAwait(false);
            return await query.ToListAsync().ConfigureAwait(false);
        }
        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().SingleOrDefaultAsync(s => s.Id == id);
        }
        public virtual async Task<T> GetByIdAsync(int id, params string[] includes)
        {
            var query = _dbContext.Set<T>().AsNoTracking().AsQueryable();

            foreach (var include in includes)
                query = query.Include(include);

            return await query.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);
        }
        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> filter = null, params string[] includes)
        {
            var query = _dbContext.Set<T>().AsNoTracking().AsQueryable();

            foreach (var include in includes)
                query = query.Include(include);

            if (filter != null)
                query = query.Where(filter);
            return await query.FirstOrDefaultAsync().ConfigureAwait(false);
        }
        public virtual async Task<T> UpdateAsync(T model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("entity");
            }
          
            _dbContext.Set<T>().Update(model);
            await _dbContext.SaveChangesAsync();

            return model;
        }
        public virtual async Task<bool> ExistAsync(int id)
        {
            return await _dbContext.Set<T>().AsNoTracking().AnyAsync(x => x.Id == id);
        }
        public virtual async Task<bool> ExistAsync(Expression<Func<T, bool>> filter)
        {
            var query = _dbContext.Set<T>().AsNoTracking().AsQueryable();
            return await query.AnyAsync(filter);
        }

        #endregion
    }
}
