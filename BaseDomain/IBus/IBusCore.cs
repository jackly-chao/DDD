using BaseDomain.Command.Model;
using BaseDomain.Event.Model;
using BaseDomain.Notification.Model;
using System.Threading.Tasks;

namespace BaseDomain.IBus
{
    /// <summary>
    /// 泛型总线(中介者)处理程序接口
    /// </summary>
    public interface IBusCore
    {
        /// <summary>
        /// 发送命令,将命令模型发布到总线(中介者)(这是请求/响应模式)
        /// </summary>
        /// <typeparam name="TCommandModel">泛型命令模型</typeparam>
        /// <param name="commandModel">命令模型</param>
        /// <returns></returns>
        Task SendCommandAsync<TCommandModel>(TCommandModel commandModel) where TCommandModel : CommandModelCore<object>;

        /// <summary>
        /// 触发事件,将事件模型发布到总线(中介者)(这是发布/订阅模式)
        /// </summary>
        /// <typeparam name="TEventModel">泛型事件模型</typeparam>
        /// <param name="eventModel">事件模型</param>
        /// <returns></returns>
        Task RaiseEventAsync<TEventModel>(TEventModel eventModel) where TEventModel : EventModelCore<object>;

        /// <summary>
        /// 触发通知,将通知模型发布到总线(中介者)(这是发布/订阅模式)
        /// </summary>
        /// <typeparam name="TNotificationModel">泛型通知模型</typeparam>
        /// <param name="notificationModel">通知模型</param>
        /// <returns></returns>
        Task RaiseNotificationAsync<TNotificationModel>(TNotificationModel notificationModel) where TNotificationModel : NotificationModelCore;
    }
}
