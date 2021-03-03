using NRLMNationalMIS.Data.Repositories.IRepositories;
using System;
using System.Linq;
using System.Linq.Expressions;
using NRLMNationalMIS.Data.Model;


namespace NRLMNationalMIS.Data.Repositories
{
    public class BaseRepository : IDisposable, IBaseRepository
    {
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected internal NRLMNationalMISEntities db = null;
        protected BaseRepository()
        {
            db = new NRLMNationalMISEntities();
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
