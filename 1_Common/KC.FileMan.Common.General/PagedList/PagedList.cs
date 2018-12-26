using System;
using System.Collections.Generic;

namespace KC.FileMan.Common.General
{
    /// <summary>
    /// PageList封装
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedList<T>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public PagedList()
        {
            this.Rows = new List<T>();
        }

        /// <summary>
        /// 当前第几页
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// 每页数量
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 总数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 页数
        /// </summary>
        public int PageCount
        {
            get
            {
                if (TotalCount > 0 && PageSize > 0 && TotalCount > PageSize)
                {
                    return (int)Math.Ceiling((decimal)TotalCount / PageSize);
                }
                return 1;
            }
        }

        /// <summary>
        /// 当前页数据
        /// </summary>
        public IList<T> Rows { get; set; }
    }
}
