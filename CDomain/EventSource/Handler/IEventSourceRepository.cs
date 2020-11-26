using CDomain.EventSource.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CDomain.EventSource.Handler
{
    /// <summary>
    /// 事件溯源模型仓储接口
    /// </summary>
    public interface IEventSourceRepository
    {
        /// <summary>
        /// 根据聚合根ID获取列表
        /// </summary>
        /// <param name="aggregateId">聚合根ID</param>
        /// <returns></returns>
        Task<List<EventSourceModel>> GetAllByAggregateIdAsync(int aggregateId);

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="eventSourceModel">事件溯源模型</param>
        Task AddAsync(EventSourceModel eventSourceModel);
    }
}
