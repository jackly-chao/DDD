using AutoMapper;
using BaseApplication.ViewModel;
using BaseApplication.IService;
using BaseDomain.IRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BaseDomain.Model;

namespace BaseApplication.Service
{
    /// <summary>
    /// 泛型服务
    /// </summary>
    /// <typeparam name="TViewModel">泛型视图模型</typeparam>
    /// <typeparam name="TIRepository">泛型仓储接口</typeparam>
    /// <typeparam name="TDomainModel">泛型领域模型</typeparam>
    public class ServiceCore<TViewModel, TIRepository, TDomainModel> : IServiceCore<TViewModel> where TViewModel : ViewModelCore<object> where TIRepository : IRepositoryCore<TDomainModel> where TDomainModel : AggregateRootCore<object>
    {
        /// <summary>
        /// 仓储接口
        /// </summary>
        protected readonly TIRepository _repository;

        /// <summary>
        /// 自动映射
        /// </summary>
        protected readonly IMapper _mapper;

        public ServiceCore(TIRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<TViewModel>> GetAllAsync()
        {
            return _mapper.Map<List<TViewModel>>(await _repository.GetAllAsync());
        }

        public async Task<TViewModel> GetByIdAsync(object id)
        {
            return _mapper.Map<TViewModel>(await _repository.GetByIdAsync(id));
        }

        public async Task AddAsync(TViewModel viewModel)
        {
            await _repository.AddAsync(_mapper.Map<TDomainModel>(viewModel));
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(TViewModel viewModel)
        {
            await _repository.UpdateAsync(_mapper.Map<TDomainModel>(viewModel));
        }

        public async Task DeleteByIdAsync(object id)
        {
            await _repository.DeleteByIdAsync(id);
        }
    }
}
