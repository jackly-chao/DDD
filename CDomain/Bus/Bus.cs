using CDomain.Command.Model;
using CDomain.Event.Model;
using CDomain.EventSource.Handler;
using CDomain.Notification.Model;
using MediatR;
using System.Threading.Tasks;

namespace CDomain.Bus
{
    /// <summary>
    /// 总线(中介者)处理程序
    /// </summary>
    public sealed class Bus : IBus.IBus
    {
        /// <summary>
        /// 中介者
        /// </summary>
        private readonly IMediator _mediator;

        /// <summary>
        /// 服务工厂
        /// </summary>
        private readonly ServiceFactory _serviceFactory;

        /// <summary>
        /// 事件模型仓储接口
        /// </summary>
        //private readonly IEventRepository _eventRepository;

        public Bus(IMediator mediator, ServiceFactory serviceFactory/*, IEventRepository eventRepository*/)
        {
            _mediator = mediator;
            _serviceFactory = serviceFactory;
            //_eventRepository = eventRepository;
        }

        public async Task SendCommandAsync<TCommandModel>(TCommandModel commandModel) where TCommandModel : CommandModel
        {
            await _mediator.Send(commandModel);
        }

        public async Task RaiseEventAsync<TEventModel>(TEventModel eventModel) where TEventModel : EventModel
        {
            //await _eventRepository.AddAsync(eventModel);
            await _mediator.Publish(eventModel);
        }

        public async Task RaiseNotificationAsync<TNotificationModel>(TNotificationModel notificationModel) where TNotificationModel : NotificationModel
        {
            await _mediator.Publish(notificationModel);
        }
    }
}
