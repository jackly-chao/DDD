using AutoMapper;
using CApplication.IService;
using CApplication.ViewModel;
using CDomain.Command.Model;
using CDomain.IBus;
using CDomain.IRepository;
using CDomain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CApplication.Service
{
    /// <summary>
    /// 用户服务
    /// </summary>
    public class UserService : IUserService
    {
        /// <summary>
        /// 用户领域模型(领域模型,命令模型,事件模型)仓储接口
        /// </summary>
        private readonly IUserRepository _repository;

        /// <summary>
        /// 自动映射
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// 总线(中介者)处理程序接口
        /// </summary>
        private readonly IBus _bus;

        public UserService(IUserRepository repository, IMapper mapper, IBus bus)
        {
            _repository = repository;
            _mapper = mapper;
            _bus = bus;
        }

        public async Task<List<UserViewModel>> GetAllAsync()
        {
            return _mapper.Map<List<UserViewModel>>(await _repository.GetAllAsync());
        }

        public async Task<UserViewModel> GetByIdAsync(int id)
        {
            return _mapper.Map<UserViewModel>(await _repository.GetByIdAsync(id));
        }

        public async Task AddAsync(UserViewModel viewModel)
        {
            await _repository.AddAsync(_mapper.Map<UserDomainModel>(viewModel));
        }

        public async Task UpdateAsync(UserViewModel viewModel)
        {
            await _repository.UpdateAsync(_mapper.Map<UserDomainModel>(viewModel));
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _repository.DeleteByIdAsync(id);
        }

        /// <summary>
        /// 异步注册
        /// </summary>
        /// <param name="viewModel">C端用户视图模型</param>
        /// <returns></returns>
        public async Task RegisterAsync(UserViewModel viewModel)
        {
            var commandModel = _mapper.Map<UserRegisterCommandModel>(viewModel);
            await _bus.SendCommandAsync(commandModel);
        }

        /// <summary>
        /// 异步登录
        /// </summary>
        /// <param name="viewModel">C端用户视图模型</param>
        /// <returns></returns>
        public async Task LoginAsync(UserViewModel viewModel)
        {
            var commandModel = _mapper.Map<UserLoginCommandModel>(viewModel);
            await _bus.SendCommandAsync(commandModel);
        }

        /// <summary>
        /// 异步退出
        /// </summary>
        /// <param name="viewModel">C端用户视图模型</param>
        /// <returns></returns>
        public async Task LogoutAsync(UserViewModel viewModel)
        {
            var commandModel = _mapper.Map<UserLogoutCommandModel>(viewModel);
            await _bus.SendCommandAsync(commandModel);
        }
    }
}
