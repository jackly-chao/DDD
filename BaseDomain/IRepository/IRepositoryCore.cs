using BaseDomain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BaseDomain.IRepository
{
    /// <summary>
    /// 泛型领域模型(领域模型,命令模型,事件模型)仓储接口,并继承IDisposable,显式释放资源
    /// </summary>
    /// <typeparam name="TDomainModel">泛型领域模型(领域模型,命令模型,事件模型)</typeparam>
    public interface IRepositoryCore<TDomainModel> : IDisposable where TDomainModel : AggregateRootCore<object>
    {
        /// <summary>
        /// 异步查询列表
        /// </summary>
        /// <returns></returns>
        Task<List<TDomainModel>> GetAllAsync();

        /// <summary>
        /// 根据ID异步查询
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        Task<TDomainModel> GetByIdAsync(object id);

        /// <summary>
        /// 异步新增
        /// </summary>
        /// <param name="domainModel">领域模型(领域模型,命令模型,事件模型)</param>
        /// <returns></returns>
        Task AddAsync(TDomainModel domainModel);

        /// <summary>
        /// 异步修改
        /// </summary>
        /// <param name="domainModel">领域模型(领域模型,命令模型,事件模型)</param>
        /// <returns></returns>
        Task UpdateAsync(TDomainModel domainModel);

        /// <summary>
        /// 根据ID异步删除
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        Task DeleteByIdAsync(object id);
    }
}
