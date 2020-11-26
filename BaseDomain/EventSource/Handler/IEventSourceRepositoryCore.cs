using BaseDomain.EventSource.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BaseDomain.EventSource.Handler
{
    /// <summary>
    /// 泛型事件溯源模型仓储接口
    /// </summary>
    /// <typeparam name="TEventSourceModel">泛型事件溯源模型</typeparam>
    public interface IEventSourceRepositoryCore<TEventSourceModel> : IDisposable where TEventSourceModel : EventSourceModelCore<object, object>
    {
        /// <summary>
        /// 根据聚合根ID获取列表
        /// </summary>
        /// <param name="aggregateId">聚合根ID</param>
        /// <returns></returns>
        Task<List<TEventSourceModel>> GetAllByAggregateIdAsync(object aggregateId);

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="eventSourceModel">事件溯源模型</param>
        Task AddAsync(TEventSourceModel eventSourceModel);
    }
}
