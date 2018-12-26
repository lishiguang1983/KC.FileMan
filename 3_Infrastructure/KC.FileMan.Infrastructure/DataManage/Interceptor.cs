using Castle.DynamicProxy;
using System;
using System.Linq;

namespace KC.FileMan.Infrastructure
{
    /// <summary>
    /// 事务拦截
    /// </summary>
    public class TransactionInterceptor : IntercepterBase, IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            var attribute = invocation.MethodInvocationTarget.GetCustomAttributes(typeof(TransactionAttribute), true)
                                                .SingleOrDefault() as TransactionAttribute;
            if (attribute == null)
            {
                invocation.Proceed();
                return;
            }

            var dataSession = WebSessionManager.GetSession("KC_FileMan"); ;
            if (dataSession.Transaction != null && dataSession.Transaction.IsActive)
            {
                invocation.Proceed();
                return;
            }

            var trans = dataSession.BeginTransaction(attribute.IsolationLevel);
            try
            {
                invocation.Proceed();
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                //Logger.Error(string.Format("事务错误： {0}", ex.InnerException ?? ex));

                throw ex;
            }

        }
    }

    /// <summary>
    /// 数据库会话拦截
    /// </summary>
    public class DataSessionInterceptor : IntercepterBase, IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            var attribute = invocation.MethodInvocationTarget.GetCustomAttributes(typeof(DataSessionAttribute), true)
                                                .SingleOrDefault() as DataSessionAttribute;
            if (attribute == null)
            {
                invocation.Proceed();
                return;
            }

            try
            {
                WebSessionManager.OpenSession("KC_FileMan");
                invocation.Proceed();
                WebSessionManager.CloseSession("KC_FileMan");

            }
            catch (Exception ex)
            {
                WebSessionManager.ClearSession();
                throw ex;
            }
        }
    }
}
