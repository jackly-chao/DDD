using CDomain.Event.Model;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CDomain.Event.Handler
{
    /// <summary>
    /// 用户事件处理程序
    /// </summary>
    public class UserEventHandler : INotificationHandler<UserRegisteredEventModel>, INotificationHandler<UserLoginedEventModel>, INotificationHandler<UserLogoutedEventModel>
    {
        /// <summary>
        ///  用户注册事件处理
        /// </summary>
        /// <param name="eventModel">用户注册事件模型</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task Handle(UserRegisteredEventModel eventModel, CancellationToken cancellationToken)
        {
            // 恭喜您，注册成功，欢迎加入我们。
            await Task.CompletedTask;
        }

        /// <summary>
        ///  用户登录事件处理
        /// </summary>
        /// <param name="eventModel">用户登录事件模型</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task Handle(UserLoginedEventModel eventModel, CancellationToken cancellationToken)
        {
            // 恭喜您，登录成功。
            await Task.CompletedTask;
        }

        /// <summary>
        ///  用户退出事件处理
        /// </summary>
        /// <param name="eventModel">用户退出事件模型</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task Handle(UserLogoutedEventModel eventModel, CancellationToken cancellationToken)
        {
            // 恭喜您，退出成功。
            await Task.CompletedTask;
        }
    }
}
