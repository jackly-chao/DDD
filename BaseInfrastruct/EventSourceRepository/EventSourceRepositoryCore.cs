using BaseDomain.EventSource.Handler;
using BaseDomain.EventSource.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseInfrastruct.EventSourceRepository
{
    /// <summary>
    /// 泛型事件溯源模型仓储
    /// </summary>
    /// <typeparam name="TEventSourceModel">泛型事件溯源模型</typeparam>
    /// <typeparam name="TEventSourceContext">泛型事件溯源数据库上下文</typeparam>
    public class EventSourceRepositoryCore<TEventSourceModel, TEventSourceContext> : IEventSourceRepositoryCore<TEventSourceModel> where TEventSourceModel : EventSourceModelCore<object, object> where TEventSourceContext : DbContext
    {
        /// <summary>
        /// 事件溯源数据库上下文
        /// </summary>
        protected readonly TEventSourceContext _db;

        /// <summary>
        /// 事件溯源模型集合
        /// </summary>
        protected readonly DbSet<TEventSourceModel> _models;

        public EventSourceRepositoryCore(TEventSourceContext context)
        {
            _db = context;
            _models = _db.Set<TEventSourceModel>();
        }

        public async Task<List<TEventSourceModel>> GetAllByAggregateIdAsync(object aggregateId)
        {
            return await _models.Where(d => d.AggregateId.Equals(aggregateId)).ToListAsync();
        }

        public async Task AddAsync(TEventSourceModel eventSourceModel)
        {
            await _models.AddAsync(eventSourceModel);
            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
