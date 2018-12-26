using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace KC.FileMan.IRepository
{
    /// <summary>
    /// 仓储新年好口基类
    /// </summary>
    public interface IRepositoryBase
    {
    }

    public interface IRepositoryBase<T> : IRepositoryBase
        where T : class
    {
        /// <summary>
        /// 根据ID获取实例
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetById(object id);

        /// <summary>
        /// 删除实例
        /// </summary>
        void Delete(T entity);

        /// <summary>
        /// 更新实例
        /// </summary>
        void Update(T entity);

        /// <summary>
        /// 批量更新实例
        /// </summary>
        /// <param name="entityList">实例列表</param>
        void UpdateList(IEnumerable<T> entityList);

        /// <summary>
        /// 保存实例
        /// </summary>
        T Save(T entity);

        /// <summary>
        /// 批量保存实例
        /// </summary>
        /// <param name="entityList">实例列表</param>
        void SaveList(IEnumerable<T> entityList);

        /// <summary>
        /// 查找实例
        /// </summary>
        /// <param name="filter">过滤方法</param>
        /// <returns></returns>
        T Find(Expression<Func<T, bool>> filter);

        /// <summary>
        /// 统计数量
        /// </summary>
        /// <param name="filter">过滤方法</param>
        /// <returns></returns>
        int Count(Expression<Func<T, bool>> filter);

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        IList<T> GetAll();

        /// <summary>
        /// 根据条件获取数据列表
        /// </summary>
        /// <param name="filter">过滤条件</param>
        /// <returns></returns>
        IList<T> GetList(Expression<Func<T, bool>> filter);

        /// <summary>
        /// 根据条件获取数据列表
        /// </summary>
        /// <param name="filter">过滤条件</param>
        /// <param name="sortExpression">排序字段</param>
        /// <param name="isAsc">是否升序，否则降序</param>
        /// <returns></returns>
        IList<T> GetList(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sortExpression, bool isAsc = true);

        /// <summary>
        /// 根据条件获取分页数据
        /// </summary>
        /// <param name="filter">过滤条件</param>
        /// <param name="page">页码</param>
        /// <param name="pageSize">页数据数量</param>
        /// <param name="sortExpression">排序条件</param>
        /// <param name="isAsc">是否升序，默认为true</param>
        /// <returns></returns>
        IList<T> GetList(
            Expression<Func<T, bool>> filter,
            int page,
            int pageSize,
            Expression<Func<T, object>> sortExpression,
            bool isAsc = true);
    }
}
