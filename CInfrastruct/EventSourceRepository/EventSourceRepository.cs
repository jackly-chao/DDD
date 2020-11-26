using CDomain.EventSource.Handler;
using CDomain.EventSource.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CInfrastruct.EventSourceRepository
{
    /// <summary>
    /// 事件溯源模型仓储
    /// </summary>
    public class EventSourceRepository : IEventSourceRepository
    {
        /// <summary>
        /// 事件溯源数据库上下文
        /// </summary>
        protected readonly EventSourceContext.EventSourceContext _db;

        /// <summary>
        /// 事件溯源模型集合
        /// </summary>
        protected readonly DbSet<EventSourceModel> _models;

        public EventSourceRepository(EventSourceContext.EventSourceContext context)
        {
            _db = context;
            _models = _db.Set<EventSourceModel>();
        }

        public async Task<List<EventSourceModel>> GetAllByAggregateIdAsync(int aggregateId)
        {
            return await _models.Where(d => d.AggregateId.Equals(aggregateId)).ToListAsync();
        }

        public async Task AddAsync(EventSourceModel eventSourceModel)
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
