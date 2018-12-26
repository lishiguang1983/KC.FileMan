using KC.FileMan.IRepository;
using NHibernate;

namespace KC.FileMan.Repository
{
    /// <summary>
    /// Repository基类
    /// </summary>
    public class RepositoryBase<T> : DefaultRepositoryImpl<T>, IRepositoryBase<T>
        where T : class
    {
        public RepositoryBase(ISessionFactory sessionFactory)
            : base(sessionFactory)
        {

        }
    }
}
