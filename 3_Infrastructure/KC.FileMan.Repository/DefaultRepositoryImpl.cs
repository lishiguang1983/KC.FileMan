using KC.FileMan.Infrastructure;
using KC.FileMan.IRepository;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace KC.FileMan.Repository
{
    /// <summary>
    /// NHibernate实现，Repository基类
    /// </summary>
    public class DefaultRepositoryImpl
    {
        private ISessionFactory sessionFactory;

        public DefaultRepositoryImpl(ISessionFactory sessionFactory)
        {
            this.sessionFactory = sessionFactory;
        }

        protected ISession DataSession
        {
            get
            {
                return WebSessionManager.ThreadLocalSession.Value;
            }
        }
    }

    public class DefaultRepositoryImpl<T> : DefaultRepositoryImpl, IRepositoryBase<T>
        where T : class
    {
        public DefaultRepositoryImpl(ISessionFactory sessionFactory)
            : base(sessionFactory)
        {
        }

        public T GetById(object id)
        {
            return DataSession.Get<T>(id);
        }

        public void Delete(T entity)
        {
            DataSession.Delete(entity);
        }

        public void Update(T entity)
        {
            DataSession.Update(entity);
        }

        public void UpdateList(IEnumerable<T> entityList)
        {
            int batchSize = 30;
            int index = 0;

            foreach (var entity in entityList)
            {
                DataSession.Update(entity);
                if (index % batchSize == 0)
                {
                    DataSession.Flush();
                    DataSession.Clear();
                }
                index++;
            }
        }

        public T Save(T entity)
        {
            DataSession.Save(entity);
            return entity;
        }

        public void SaveList(IEnumerable<T> entityList)
        {
            int batchSize = 30;
            int index = 0;

            foreach (var entity in entityList)
            {
                DataSession.Save(entity);
                if (index % batchSize == 0)
                {
                    DataSession.Flush();
                    DataSession.Clear();
                }
                index++;
            }
        }

        public T Find(Expression<Func<T, bool>> filter)
        {
            return DataSession.QueryOver<T>().Where(filter).SingleOrDefault();
        }

        public int Count(Expression<Func<T, bool>> filter)
        {
            return DataSession.QueryOver<T>().Where(filter).RowCount();
        }

        public IList<T> GetAll()
        {
            return DataSession.QueryOver<T>().List();
        }

        public IList<T> GetList(Expression<Func<T, bool>> filter)
        {
            return DataSession.QueryOver<T>().Where(filter).List();
        }

        public IList<T> GetList(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sortExpression, bool isAsc = true)
        {
            var query = DataSession.QueryOver<T>().Where(filter);
            if (isAsc)
            {
                query = query.OrderBy(sortExpression).Asc;

            }
            else
            {
                query = query.OrderBy(sortExpression).Desc;
            }

            return query.List();
        }

        public IList<T> GetList(
                Expression<Func<T, bool>> filter,
                int page,
                int pageSize,
                Expression<Func<T, object>> sortExpression,
                bool isAsc = true
            )
        {
            var query = DataSession.QueryOver<T>()
                                .Where(filter);

            if (isAsc)
            {
                query = query.OrderBy(sortExpression).Asc;

            }
            else
            {
                query = query.OrderBy(sortExpression).Desc;
            }

            return query.Skip(pageSize * (page - 1))
                        .Take(pageSize)
                        .List();
        }
    }
}
