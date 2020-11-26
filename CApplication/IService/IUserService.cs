using CApplication.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CApplication.IService
{
    /// <summary>
    /// 用户服务接口
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// 异步查询列表
        /// </summary>
        /// <returns></returns>
        Task<List<UserViewModel>> GetAllAsync();

        /// <summary>
        /// 根据ID异步查询
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        Task<UserViewModel> GetByIdAsync(int id);

        /// <summary>
        /// 异步添加
        /// </summary>
        /// <param name="viewModel">用户视图模型</param>
        /// <returns></returns>
        Task AddAsync(UserViewModel viewModel);

        /// <summary>
        /// 异步修改
        /// </summary>
        /// <param name="viewModel">用户视图模型</param>
        Task UpdateAsync(UserViewModel viewModel);

        /// <summary>
        /// 根据ID异步删除
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        Task DeleteByIdAsync(int id);

        /// <summary>
        /// 异步注册
        /// </summary>
        /// <param name="viewModel">用户视图模型</param>
        /// <returns></returns>
        Task RegisterAsync(UserViewModel viewModel);

        /// <summary>
        /// 异步登录
        /// </summary>
        /// <param name="viewModel">用户视图模型</param>
        Task LoginAsync(UserViewModel viewModel);

        /// <summary>
        /// 异步退出
        /// </summary>
        /// <param name="viewModel">用户视图模型</param>
        Task LogoutAsync(UserViewModel viewModel);
    }
}
