using BaseDomain.Command.Model;
using BaseDomain.Event.Model;
using BaseDomain.EventSource.Handler;
using BaseDomain.IBus;
using BaseDomain.Notification.Model;
using MediatR;
using System.Threading.Tasks;

namespace BaseDomain.Bus
{
    /// <summary>
    /// 泛型总线(中介者)处理程序
    /// </summary>
    /// <typeparam name="TIEventRepository">泛型事件模型仓储接口</typeparam>
    public class BusCore<TIEventRepository> : IBusCore where TIEventRepository : IEventRepositoryCore<EventModelCore<object>>
    {
        /// <summary>
        /// 中介者
        /// </summary>
        protected readonly IMediator _mediator;

        /// <summary>
        /// 服务工厂
        /// </summary>
        protected readonly ServiceFactory _serviceFactory;

        /// <summary>
        /// 事件模型仓储接口
        /// </summary>
        protected readonly TIEventRepository _eventRepository;

        public BusCore(IMediator mediator, ServiceFactory serviceFactory, TIEventRepository eventRepository)
        {
            _mediator = mediator;
            _serviceFactory = serviceFactory;
            _eventRepository = eventRepository;
        }

        public async Task SendCommandAsync<TCommandModel>(TCommandModel commandModel) where TCommandModel : CommandModelCore<object>
        {
            await _mediator.Send(commandModel);
        }

        public async Task RaiseEventAsync<TEventModel>(TEventModel eventModel) where TEventModel : EventModelCore<object>
        {
            await _eventRepository.AddAsync(eventModel);
            await _mediator.Publish(eventModel);
        }

        public async Task RaiseNotificationAsync<TNotificationModel>(TNotificationModel notificationModel) where TNotificationModel : NotificationModelCore
        {
            await _mediator.Publish(notificationModel);
        }
    }
}
