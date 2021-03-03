using System;
using System.Collections.Generic;
using System.Linq;
using NRLMNationalMIS.Data.Repositories.IRepositories;
using NRLMNationalMIS.Data.Model;
using System.Linq.Expressions;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace NRLMNationalMIS.Data.Repositories
{
    public class GenericRepository : BaseRepository, IGenericRepository
    {
        public GenericRepository()
        {
        }

        public virtual IEnumerable<TEntity> Get<TEntity>(
         Expression<Func<TEntity, bool>> filter = null,
         Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
         string includeProperties = "") where TEntity : class
        {
            IQueryable<TEntity> query = db.Set<TEntity>();
            if (filter != null)
            {
                query = query.Where(filter);
            }
            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual IEnumerable<TEntity> GetReportResult<TEntity>(int pageNum, int pageSize, out int rowsCount,
            Expression<Func<TEntity, bool>> filter = null,
             string sortOn = "", bool isAscendingOrder = false, string includeProperties = "") where TEntity : class
        {
            IEnumerable<TEntity> pagedResult;
            rowsCount = 0;
            if (pageSize <= 0) pageSize = 20;
            IQueryable<TEntity> query = db.Set<TEntity>();
            if (filter != null)
            {
                query = query.Where(filter);
            }
            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            rowsCount = query.Count();
            if (rowsCount <= pageSize || pageNum <= 0) pageNum = 1;
            int excludedRows = (pageNum - 1) * pageSize;

            if (!string.IsNullOrEmpty(sortOn))
            {
                var param = Expression.Parameter(typeof(TEntity), "i");

                MemberExpression property = null;
                string[] fieldNames = sortOn.Split('.');
                foreach (string filed in fieldNames)
                {
                    if (property == null)
                    {
                        property = Expression.Property(param, filed);
                    }
                    else
                    {
                        property = Expression.Property(property, filed);
                    }
                }

                Expression conversion = Expression.Convert(property, typeof(object));//Expression.Property(param, fieldName)
                var mySortExpression = Expression.Lambda<Func<TEntity, object>>(conversion, param).Compile();
                pagedResult = isAscendingOrder ? query.OrderBy(mySortExpression).ToList() : query.OrderByDescending(mySortExpression).ToList();
            }
            else
            {
                pagedResult = query.ToList();
            }
            //return pagedResult.Skip(excludedRows).Take(pageSize);
            return pagedResult;
        }

        public virtual IEnumerable<TEntity> GetPagedResult<TEntity>(int pageNum, int pageSize, out int rowsCount,
            Expression<Func<TEntity, bool>> filter = null,
             string sortOn = "", bool isAscendingOrder = false, string includeProperties = "") where TEntity : class
        {
            int pn = pageNum, ps = pageSize;
            IEnumerable<TEntity> pagedResult;
            rowsCount = 0;
            if (pageSize <= 0) pageSize = 20;
            IQueryable<TEntity> query = db.Set<TEntity>();
            if (filter != null)
            {
                query = query.Where(filter);
            }
            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            rowsCount = query.Count();
            if (rowsCount <= pageSize || pageNum <= 0) pageNum = 1;
            int excludedRows = (pageNum - 1) * pageSize;

            if (!string.IsNullOrEmpty(sortOn))
            {
                var param = Expression.Parameter(typeof(TEntity), "i");

                MemberExpression property = null;
                string[] fieldNames = sortOn.Split('.');
                foreach (string filed in fieldNames)
                {
                    if (property == null)
                    {
                        property = Expression.Property(param, filed);
                    }
                    else
                    {
                        property = Expression.Property(property, filed);
                    }
                }

                Expression conversion = Expression.Convert(property, typeof(object));//Expression.Property(param, fieldName)
                var mySortExpression = Expression.Lambda<Func<TEntity, object>>(conversion, param).Compile();
                pagedResult = isAscendingOrder ? query.OrderBy(mySortExpression).ToList() : query.OrderByDescending(mySortExpression).ToList();
            }
            else
            {
                pagedResult = query.ToList();
            }
            if (pn == 0 && ps == 0)
            {
                return pagedResult;
            }
            else
            {
                return pagedResult.Skip(excludedRows).Take(pageSize);
            }

        }

        public virtual TEntity GetByID<TEntity>(object id) where TEntity : class
        {
            return db.Set<TEntity>().Find(id);
        }

        public virtual TEntity GetFirstOrDefault<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class
        {
            return db.Set<TEntity>().FirstOrDefault(filter);
        }
        public virtual bool Exists<TEntity>(Expression<Func<TEntity, bool>> filter = null) where TEntity : class
        {
            if (filter == null) return false;
            return (db.Set<TEntity>().Any(filter));
        }

        public virtual int Insert<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            try
            {

                db.Set<TEntity>().Add(entity);
                db.SaveChanges();
                int ret = 0;
                PropertyInfo key = typeof(TEntity).GetProperties().FirstOrDefault(p =>
    p.CustomAttributes.Any(attr => attr.AttributeType == typeof(KeyAttribute)));

                //            var key = typeof(TEntity).GetProperties().FirstOrDefault(p =>
                //p.Name.Equals("ID", StringComparison.OrdinalIgnoreCase)
                // || p.Name.Equals(typeof(TEntity).Name + "ID", StringComparison.OrdinalIgnoreCase));
                if (key != null)
                {
                    ret = (int)key.GetValue(entity, null);
                }
                return ret;
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
                var fail = new Exception(msg, dbEx);
                throw fail;
            }
        }

        public virtual bool AddMultipleEntity<TEntity>(IEnumerable<TEntity> entityList) where TEntity : class
        {
            var flag = false;
            if (entityList == null)
            {
                throw new ArgumentNullException("entity");
            }
            try
            {
                db.Set<TEntity>().AddRange(entityList);
                db.SaveChanges();
                flag = true;
            }
            catch (Exception)
            {
                throw;
            }
            return flag;
        }

        public virtual bool RemoveMultipleEntity<TEntity>(IEnumerable<TEntity> removeEntityList) where TEntity : class
        {
            var flag = false;
            if (removeEntityList == null)
            {
                throw new ArgumentNullException("entity");
            }
            try
            {
                db.Set<TEntity>().RemoveRange(removeEntityList);
                db.SaveChanges();
                flag = true;
            }
            catch (Exception)
            {
                throw;
            }
            return flag;
        }
        public virtual void Delete<TEntity>(object id) where TEntity : class
        {
            TEntity entityToDelete = db.Set<TEntity>().Find(id);
            Delete(entityToDelete);
            db.SaveChanges();
        }
        public virtual void Delete<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class
        {
            var query = db.Set<TEntity>().Where(filter);
            db.Set<TEntity>().RemoveRange(query);
            db.SaveChanges();
        }

        public virtual void Delete<TEntity>(TEntity entityToDelete) where TEntity : class
        {
            if (entityToDelete == null)
            {
                throw new ArgumentNullException("entity");
            }
            try
            {
                //if (db.Entry(entityToDelete).State == EntityState.Detached)
                //{
                //    dbSet.Attach(entityToDelete);
                //}
                db.Set<TEntity>().Remove(entityToDelete);
                db.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
                var fail = new Exception(msg, dbEx);
                throw fail;
            }
        }
        public virtual IEnumerable<T> ExecWithStoreProcedure<T>(string query, params object[] parameters)
        {
            return db.Database.SqlQuery<T>(query, parameters);
        }
        public virtual void ExecuteWithStoreProcedure(string query, params object[] parameters)
        {
            db.Database.ExecuteSqlCommand(query, parameters);
        }
        public virtual void Update<TEntity>(TEntity entityToUpdate) where TEntity : class
        {
            if (entityToUpdate == null)
            {
                throw new ArgumentNullException("entity");
            }
            try
            {
                db.Entry(entityToUpdate).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
                var fail = new Exception(msg, dbEx);
                throw fail;
            }
        }

        public virtual DbParameter GetParameter()
        {
            return new SqlParameter();
        }
        public virtual DbParameter GetParameter(string paramName, System.Data.DbType dbtype, object value = null, bool bOutput = false)
        {
            var param = new SqlParameter();
            param.ParameterName = paramName;
            if (value != null)
            { param.Value = value; }
            param.DbType = dbtype;
            if (bOutput)
            { param.Direction = System.Data.ParameterDirection.Output; }
            return param;
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                    db.Dispose();
            }
            this.disposed = true;
        }

        public new void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public DataTable GetTablesSchema(string sTableName)
        {
            DataTable dbSqlDataSet = new DataTable();
            try
            {

                SqlCommand dbSqlCommand;
                SqlDataAdapter dbSqlAdapter;
                SqlConnection dbSqlconnection;
                dbSqlconnection = new SqlConnection(db.Database.Connection.ConnectionString);
                dbSqlCommand = new SqlCommand();
                dbSqlCommand.Connection = dbSqlconnection;
                dbSqlCommand.CommandType = CommandType.StoredProcedure;
                dbSqlCommand.CommandText = "[GetSchemaInformation]";
                dbSqlCommand.Parameters.Add("@tableNameList", SqlDbType.VarChar).Value = sTableName;
                dbSqlAdapter = new SqlDataAdapter(dbSqlCommand);
                if (dbSqlconnection.State == ConnectionState.Closed)
                    dbSqlconnection.Open();
                dbSqlAdapter.Fill(dbSqlDataSet);
                dbSqlconnection.Close();
            }
            catch (Exception ex)
            {
                log.Error("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw ex;
            }
            return dbSqlDataSet;
        }


    }
}
