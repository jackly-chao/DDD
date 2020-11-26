using CDomain.Event.Model;
using CDomain.EventSource.Model;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace CDomain.EventSource.Handler
{
    /// <summary>
    /// 事件模型仓储接口
    /// </summary>
    public interface IEventRepository
    {
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="eventModel">事件模型</param>
        //Task AddAsync(EventModel eventModel);
    }

    /// <summary>
    /// 事件模型仓储
    /// </summary>
    public class EventRepository : IEventRepository
    {
        /// <summary>
        /// 事件溯源模型仓储接口
        /// </summary>
        private readonly IEventSourceRepository _eventSourceRepository;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public EventRepository(IEventSourceRepository eventSourceRepository, IHttpContextAccessor httpContextAccessor)
        {
            _eventSourceRepository = eventSourceRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task AddAsync(EventModel eventModel)
        {
            var eventSourceModel = new EventSourceModel(eventModel, _httpContextAccessor.HttpContext.User.Identity.Name ?? "");
            await _eventSourceRepository.AddAsync(eventSourceModel);
        }
    }
}
