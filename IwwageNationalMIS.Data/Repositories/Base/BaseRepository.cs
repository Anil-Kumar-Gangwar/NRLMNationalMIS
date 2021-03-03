using IwwageNationalMIS.Data.Repositories.IRepositories;
using System;
using System.Linq;
using System.Linq.Expressions;
using IwwageNationalMIS.Data.Model;


namespace IwwageNationalMIS.Data.Repositories
{
    public class BaseRepository : IDisposable, IBaseRepository
    {
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected internal IwwageNationalMISEntities db = null;
        protected BaseRepository()
        {
            db = new IwwageNationalMISEntities();
        }
        public IQueryable<T> PagedResult<T, TResult>(IQueryable<T> query, int pageNum, int pageSize, Expression<Func<T, TResult>> orderByProperty, bool isAscendingOrder, out int rowsCount)
        {
            if (pageSize <= 0) pageSize = 20;
            rowsCount = query.Count();
            int excludedRows = (pageNum - 1) * pageSize;
            query = isAscendingOrder ? query.OrderBy(orderByProperty) : query.OrderByDescending(orderByProperty);
            return query.Skip(excludedRows).Take(pageSize);
        }

        public void Dispose()
        {

        }
    }
}
