using BaseDomain.Command.Model;
using BaseDomain.Notification.Model;
using BaseDomain.IBus;
using System.Threading.Tasks;
using BaseDomain.Event.Model;

namespace BaseDomain.Command.Handler
{
    /// <summary>
    /// 泛型命令处理程序
    /// </summary>
    /// <typeparam name="TIUnitOfWork">泛型工作单元接口</typeparam>
    /// <typeparam name="TIBus">泛型总线(中介者)处理程序接口</typeparam>
    public class CommandHandlerCore<TIUnitOfWork, TIBus> where TIUnitOfWork : IUnitOfWork.IUnitOfWorkCore where TIBus : IBusCore
    {
        /// <summary>
        /// 工作单元接口
        /// </summary>
        protected readonly TIUnitOfWork _unitOfWork;

        /// <summary>
        /// 总线(中介者)处理程序接口
        /// </summary>
        protected readonly TIBus _bus;

        public CommandHandlerCore(TIUnitOfWork unitOfWork, TIBus bus)
        {
            _unitOfWork = unitOfWork;
            _bus = bus;
        }

        /// <summary>
        /// 工作单元提交
        /// </summary>
        /// <returns></returns>
        public async Task<int> CommitAsync()
        {
            return await _unitOfWork.CommitAsync();
        }

        /// <summary>
        /// 工作单元事务提交
        /// </summary>
        /// <returns></returns>
        public async Task<int> CommitByTransactionAsync()
        {
            return await _unitOfWork.CommitByTransactionAsync();
        }

        /// <summary>
        /// 命令模型验证失败,触发通知,发布到总线
        /// </summary>
        /// <typeparam name="TCommandModel">泛型命令模型</typeparam>
        /// <param name="commandModel">命令模型</param>
        /// <returns></returns>
        protected async Task HandleValidateFailedAsync<TCommandModel>(TCommandModel commandModel) where TCommandModel : CommandModelCore<object>
        {
            foreach (var error in commandModel.ValidationResult.Errors)
            {
                await _bus.RaiseNotificationAsync(new NotificationModelCore(commandModel.MessageType, error.ErrorMessage));
            }
        }

        /// <summary>
        /// 命令模型处理失败,触发通知,发布到总线
        /// </summary>
        /// <typeparam name="TNotificationModel">泛型通知模型</typeparam>
        /// <param name="notificationModel">通知模型</param>
        /// <returns></returns>
        protected async Task HandleFailedAsync<TNotificationModel>(TNotificationModel notificationModel) where TNotificationModel : NotificationModelCore
        {
            await _bus.RaiseNotificationAsync(notificationModel);
        }

        /// <summary>
        /// 命令模型处理成功,触发事件,发布到总线
        /// </summary>
        /// <typeparam name="TEventModel">泛型事件模型</typeparam>
        /// <param name="eventModel">事件模型</param>
        /// <returns></returns>
        protected async Task HandleSucceedAsync<TEventModel>(TEventModel eventModel) where TEventModel : EventModelCore<object>
        {
            await _bus.RaiseEventAsync(eventModel);
        }
    }
}
