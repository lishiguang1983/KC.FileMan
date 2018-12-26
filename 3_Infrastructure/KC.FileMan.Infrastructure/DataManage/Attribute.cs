using System;
using System.Data;

namespace KC.FileMan.Infrastructure
{
    /// <summary>
    /// 事务属性，用在需要事务支持的业务逻辑
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class TransactionAttribute : Attribute
    {
        /// <summary>
        /// 默认命令过期时间120s
        /// </summary>
        private const int DefaultTimeout = 120;

        /// <summary>
        /// 事务级别
        /// </summary>
        public IsolationLevel IsolationLevel { get; private set; }

        /// <summary>
        ///  超时时间，单位秒
        /// </summary>
        public int Timeout { get; private set; }

        public TransactionAttribute()
        {
            this.IsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.Timeout = DefaultTimeout;
        }

        public TransactionAttribute(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted, int timeout = DefaultTimeout)
        {
            this.IsolationLevel = isolationLevel;
            this.Timeout = timeout;
        }
    }

    /// <summary>
    /// 数据库会话特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class DataSessionAttribute : Attribute
    {
    }
}
