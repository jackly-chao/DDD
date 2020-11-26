using BaseDomain.Notification.Model;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BaseDomain.Notification.Handler
{
    /// <summary>
    /// 泛型通知处理程序
    /// </summary>
    /// <typeparam name="TNotificationModel">泛型通知模型</typeparam>
    public class NotificationHandlerCore<TNotificationModel> : INotificationHandler<TNotificationModel> where TNotificationModel : NotificationModelCore
    {
        /// <summary>
        /// 通知模型列表
        /// </summary>
        private List<TNotificationModel> _notificationModels;

        public NotificationHandlerCore()
        {
            _notificationModels = new List<TNotificationModel>();
        }

        /// <summary>
        /// 处理方法
        /// </summary>
        /// <param name="notification">通知模型</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task Handle(TNotificationModel notification, CancellationToken cancellationToken)
        {
            _notificationModels.Add(notification);
            await Task.CompletedTask;
        }

        /// <summary>
        /// 获取当前生命周期内的通知列表
        /// </summary>
        /// <returns></returns>
        public virtual List<TNotificationModel> GetAll()
        {
            return _notificationModels;
        }

        /// <summary>
        /// 判断当前生命周期内是否存在通知
        /// </summary>
        /// <returns></returns>
        public virtual bool HasNotifications()
        {
            return _notificationModels.Any();
        }

        /// <summary>
        /// 回收(清空通知)
        /// </summary>
        public void Dispose()
        {
            _notificationModels = null;
        }
    }
}
