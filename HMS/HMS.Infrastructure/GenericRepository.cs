using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Infrastructure
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        #region Fields
        protected readonly ApplicationDbContext _context;
        protected DbSet<T> _dbSet;
        #endregion

        #region Constructor
        public GenericRepository(ApplicationDbContext Context)
        {
            _context = Context;
            _dbSet = _context.Set<T>();
        }
        #endregion

        #region Methods

        #region Get
        public async Task<T> GetDataById(int id)
        {
            try
            {
                var c = await _dbSet.FindAsync(id);
                return c;
            }
            catch
            {

                throw;
            }
        }

        public async Task<T> GetDataById(string id)
        {
            try
            {
                var c = await _dbSet.FindAsync(id);
                return c;
            }
            catch
            {

                throw;
            }
        }

        public async Task<IEnumerable<T>> GetData()
        {
            try
            {
                var i = await _dbSet.ToListAsync();
                return i;
            }
            catch
            {

                throw;
            }
        }
        #endregion

        #region GetByExpression
        public async Task<T> GetByExpression(Expression<Func<T, bool>> expression)
        {
            try
            {
                var c = await _dbSet.Where(expression).FirstOrDefaultAsync();
                return c;
            }
            catch
            {

                throw;
            }
        }

        public async Task<IEnumerable<T>> GetAllByExpression(Expression<Func<T, bool>> expression)
        {
            try
            {
                var c = await _dbSet.Where(expression).ToListAsync();
                return c;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region Add
        public async Task<T> AddData(T entity)
        {
            try
            {
                var result = await _dbSet.AddAsync(entity);
                return result.Entity;
            }
            catch
            {
                throw;
            }

        }
        #endregion

        #region Update
        public async Task<T> EditData(T value)
        {
            try
            {
                if (value != null)
                {
                    await Task.Yield();
                    var pp = value;
                    _dbSet.Update(pp);
                    return value;

                }
                else
                {
                    return null;
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region Delete
        public async Task<T> DeleteData(int id)
        {
            try
            {
                var exist = await _dbSet.FindAsync(id);
                if (exist != null)
                {
                    _dbSet.Remove(exist);
                    return exist;
                }

                return null;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region Count
        public async Task<int> CountAll()
        {
            try
            {
                var c = await _dbSet.CountAsync();
                return c;
            }
            catch
            {

                throw;
            }
        }
        #endregion

        #endregion
    }
}
