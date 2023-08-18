using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Infrastructure
{
    public interface IGenericRepository<T> where T : class
    {
        #region Get
        Task<T> GetDataById(int id);
        Task<T> GetDataById(string id);
        Task<IEnumerable<T>> GetData();
        #endregion

        #region GetByExpression
        Task<T> GetByExpression(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> GetAllByExpression(Expression<Func<T, bool>> expression);
        #endregion

        #region Add
        Task<T> AddData(T entity);

        #endregion

        #region Update
        Task<T> EditData(T value);

        #endregion

        #region Delete
        Task<T> DeleteData(int id);
        #endregion
    }
}
