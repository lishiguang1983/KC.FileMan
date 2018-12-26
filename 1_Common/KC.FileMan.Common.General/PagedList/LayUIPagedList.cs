using System.Collections.Generic;

namespace KC.FileMan.Common.General
{
    /// <summary>
    /// 前端LayUIPageList封装，用于后台返回给前端正确格式的Json数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LayUIPagedList<T>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public LayUIPagedList()
        {
            this.data = new List<T>();
        }
        /// <summary>
        /// Code
        /// </summary>
        public int code { get; set; }

        /// <summary>
        /// 信息
        /// </summary>
        public string msg { get; set; }

        /// <summary>
        /// 总数
        /// </summary>
        public int count { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public IList<T> data { get; set; }
    }
}
