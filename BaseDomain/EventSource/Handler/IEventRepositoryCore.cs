using BaseDomain.Event.Model;
using BaseDomain.EventSource.Model;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace BaseDomain.EventSource.Handler
{
    /// <summary>
    /// 泛型事件模型仓储接口
    /// </summary>
    /// <typeparam name="TEventModel">泛型事件模型</typeparam>
    public interface IEventRepositoryCore<TEventModel> where TEventModel : EventModelCore<object>
    {
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="eventModel">事件模型</param>
        Task AddAsync(TEventModel eventModel);
    }

    /// <summary>
    /// 泛型事件模型仓储
    /// </summary>
    /// <typeparam name="TEventModel">泛型事件模型</typeparam>
    /// <typeparam name="TIEventSourceRepository">泛型事件溯源模型仓储接口</typeparam>
    public class EventRepositoryCore<TEventModel, TIEventSourceRepository> : IEventRepositoryCore<TEventModel> where TEventModel : EventModelCore<object> where TIEventSourceRepository : IEventSourceRepositoryCore<EventSourceModelCore<object, object>>
    {
        /// <summary>
        /// 事件溯源模型仓储接口
        /// </summary>
        protected readonly TIEventSourceRepository _eventSourceRepository;

        protected readonly IHttpContextAccessor _httpContextAccessor;

        public EventRepositoryCore(TIEventSourceRepository eventSourceRepository, IHttpContextAccessor httpContextAccessor)
        {
            _eventSourceRepository = eventSourceRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task AddAsync(TEventModel eventModel)
        {
            var eventSourceModel = new EventSourceModelCore<object, object>(eventModel, _httpContextAccessor.HttpContext.User.Identity.Name ?? "");
            await _eventSourceRepository.AddAsync(eventSourceModel);
        }
    }
}
