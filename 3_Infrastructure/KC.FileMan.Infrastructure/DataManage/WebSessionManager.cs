using NHibernate;
using NHibernate.Context;
using System.Threading;

namespace KC.FileMan.Infrastructure
{
    /// <summary>
    /// 数据库会话管理器
    /// </summary>
    public sealed class WebSessionManager
    {
        public static ThreadLocal<ISession> ThreadLocalSession= new ThreadLocal<ISession>();

        /// <summary>
        /// 打开数据库连接
        /// </summary>
        public static void OpenSession(string key)
        {
            var factory = ModelBuilder.GetSessionFactory(key);
            if (ThreadLocalSession.Value == null)
            {
                ThreadLocalSession.Value = factory.OpenSession();
            }
        }

        public static ISession GetSession(string key)
        {
            if (ThreadLocalSession.Value != null)
            {
               return ThreadLocalSession.Value;
            }
            return null;
        }

        /// <summary>
        /// 关闭当前数据库会话
        /// </summary
        public static void CloseSession(string key)
        {
            if (ThreadLocalSession.Value != null)
            {
                var session = ThreadLocalSession.Value;
                session.Flush();
                session.Close();
                ThreadLocalSession.Value = null;
            }
        }

        public static void ClearSession()
        {
            if (ThreadLocalSession.Value != null)
            {
                var session = ThreadLocalSession.Value;
                session.Clear();
                session.Close();
                ThreadLocalSession.Value = null;
            }
        }
    }
}
