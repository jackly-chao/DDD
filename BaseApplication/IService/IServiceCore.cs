using System.Collections.Generic;
using System.Threading.Tasks;

namespace BaseApplication.IService
{
    /// <summary>
    /// 泛型服务接口
    /// </summary>
    /// <typeparam name="TViewModel">泛型视图模型</typeparam>
    public interface IServiceCore<TViewModel>
    {
        /// <summary>
        /// 异步查询列表
        /// </summary>
        /// <returns></returns>
        Task<List<TViewModel>> GetAllAsync();

        /// <summary>
        /// 根据ID异步查询
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        Task<TViewModel> GetByIdAsync(object id);

        /// <summary>
        /// 异步添加
        /// </summary>
        /// <param name="viewModel">泛型视图模型</param>
        /// <returns></returns>
        Task AddAsync(TViewModel viewModel);

        /// <summary>
        /// 异步修改
        /// </summary>
        /// <param name="viewModel">泛型视图模型</param>
        Task UpdateAsync(TViewModel viewModel);

        /// <summary>
        /// 根据ID异步删除
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        Task DeleteByIdAsync(object id);
    }
}
