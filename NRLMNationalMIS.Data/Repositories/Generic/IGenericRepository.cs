using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace NRLMNationalMIS.Data.Repositories.IRepositories
{
    public interface IGenericRepository
    {

        IEnumerable<TEntity> Get<TEntity>(
             Expression<Func<TEntity, bool>> filter = null,
             Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "") where TEntity : class;
        IEnumerable<TEntity> GetPagedResult<TEntity>(int pageNum, int pageSize, out int rowsCount,
            Expression<Func<TEntity, bool>> filter = null,
           string sortOn = "", bool isAscendingOrder = false, string includeProperties = "") where TEntity : class;
        IEnumerable<TEntity> GetReportResult<TEntity>(int pageNum, int pageSize, out int rowsCount,
            Expression<Func<TEntity, bool>> filter = null,
           string sortOn = "", bool isAscendingOrder = false, string includeProperties = "") where TEntity : class;
        TEntity GetByID<TEntity>(object id) where TEntity : class;
        TEntity GetFirstOrDefault<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class;
        bool Exists<TEntity>(Expression<Func<TEntity, bool>> filter = null) where TEntity : class;
        int Insert<TEntity>(TEntity entity) where TEntity : class;
        bool AddMultipleEntity<TEntity>(IEnumerable<TEntity> entityList) where TEntity : class;
        void Delete<TEntity>(object id) where TEntity : class;
        void Delete<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class;
        void Delete<TEntity>(TEntity entityToDelete) where TEntity : class;
        void Update<TEntity>(TEntity entityToUpdate) where TEntity : class;
        DbParameter GetParameter();
        DbParameter GetParameter(string paramName, System.Data.DbType dbtype, object value = null, bool bOutput = false);
        IEnumerable<T> ExecWithStoreProcedure<T>(string query, params object[] parameters);
        void ExecuteWithStoreProcedure(string query, params object[] parameters);
        void Dispose();
        bool RemoveMultipleEntity<TEntity>(IEnumerable<TEntity> removeEntityList) where TEntity : class;
        DataTable GetTablesSchema(string sTableName);

    }
}
