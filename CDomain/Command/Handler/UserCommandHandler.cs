using CDomain.Command.Model;
using CDomain.Event.Model;
using CDomain.IRepository;
using CDomain.Model;
using CDomain.Notification.Model;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CDomain.Command.Handler
{
    /// <summary>
    /// 用户命令处理程序
    /// </summary>
    public class UserCommandHandler : IRequestHandler<UserRegisterCommandModel, bool>, IRequestHandler<UserLoginCommandModel, bool>, IRequestHandler<UserLogoutCommandModel, bool>
    {
        /// <summary>
        /// 用户领域模型仓储接口
        /// </summary>
        private readonly IUserRepository _repository;

        /// <summary>
        /// 工作单元接口
        /// </summary>
        private readonly IUnitOfWork.IUnitOfWork _unitOfWork;

        /// <summary>
        /// 总线(中介者)处理程序接口
        /// </summary>
        private readonly IBus.IBus _bus;

        public UserCommandHandler(IUserRepository repository, IUnitOfWork.IUnitOfWork unitOfWork, IBus.IBus bus)
        {
            _repository = repository;
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
        /// <param name="commandModel">命令模型</param>
        /// <returns></returns>
        public async Task HandleValidateFailedAsync(CommandModel commandModel)
        {
            foreach (var error in commandModel.ValidationResult.Errors)
            {
                await _bus.RaiseNotificationAsync(new NotificationModel(commandModel.MessageType, error.ErrorMessage));
            }
        }

        /// <summary>
        /// 命令模型处理失败,触发通知,发布到总线
        /// </summary>
        /// <param name="notificationModel">通知模型</param>
        /// <returns></returns>
        public async Task HandleFailedAsync(NotificationModel notificationModel)
        {
            await _bus.RaiseNotificationAsync(notificationModel);
        }

        /// <summary>
        /// 命令模型处理成功,触发事件,发布到总线
        /// </summary>
        /// <param name="eventModel">事件模型</param>
        /// <returns></returns>
        public async Task HandleSucceedAsync(EventModel eventModel)
        {
            await _bus.RaiseEventAsync(eventModel);
        }

        /// <summary>
        /// 用户注册命令处理
        /// </summary>
        /// <param name="request">用户注册命令模型</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> Handle(UserRegisterCommandModel request, CancellationToken cancellationToken)
        {
            //验证
            if (!request.IsValid())
            {
                await HandleValidateFailedAsync(request);
                return await Task.FromResult(false);
            }
            //判断
            if (_repository.GetByMobileAsync(request.Mobile) != null)
            {
                await HandleFailedAsync(new NotificationModel(nameof(UserRegisterCommandModel), "该手机号已被注册!"));
                return await Task.FromResult(false);
            }
            var user = new UserDomainModel(0, request.UserName, request.Password, request.Mobile, request.NickName, request.AvatarPath);
            await _repository.AddAsync(user);
            //提交才会真正保存到数据库
            if (await CommitAsync() > 0)
            {
                await HandleSucceedAsync(new UserRegisteredEventModel(user.Id, user.UserName, user.Password, user.Mobile, user.NickName, user.AvatarPath));
            }
            return await Task.FromResult(true);
        }

        /// <summary>
        /// 用户登录命令处理
        /// </summary>
        /// <param name="request">用户登录命令模型</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> Handle(UserLoginCommandModel request, CancellationToken cancellationToken)
        {
            //验证
            if (!request.IsValid())
            {
                await HandleValidateFailedAsync(request);
                return await Task.FromResult(false);
            }
            var cUser = await _repository.GetByUserNameAsync(request.UserName);
            //判断
            if (cUser == null)
            {
                await HandleFailedAsync(new NotificationModel(nameof(UserLoginCommandModel), "该用户名不存在!"));
                return await Task.FromResult(false);
            }
            if (cUser.Password != request.Password)
            {
                await HandleFailedAsync(new NotificationModel(nameof(UserLoginCommandModel), "密码错误!"));
                return await Task.FromResult(false);
            }
            //提交才会真正保存到数据库
            if (await CommitAsync() > 0)
            {
                await HandleSucceedAsync(new UserLoginedEventModel(cUser.Id, cUser.UserName, cUser.Password));
            }
            return await Task.FromResult(true);
        }

        /// <summary>
        /// 用户退出命令处理
        /// </summary>
        /// <param name="request">用户退出命令模型</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> Handle(UserLogoutCommandModel request, CancellationToken cancellationToken)
        {
            //验证
            if (!request.IsValid())
            {
                await HandleValidateFailedAsync(request);
                return await Task.FromResult(false);
            }
            await HandleSucceedAsync(new UserLogoutedEventModel(request.Id, request.UserName));
            return await Task.FromResult(true);
        }
    }
}
