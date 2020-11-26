using CDomain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CDomain.IRepository
{
    /// <summary>
    /// 用户领域模型(领域模型,命令模型,事件模型)仓储接口
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// 异步查询列表
        /// </summary>
        /// <returns></returns>
        Task<List<UserDomainModel>> GetAllAsync();

        /// <summary>
        /// 根据ID异步查询
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        Task<UserDomainModel> GetByIdAsync(int id);

        /// <summary>
        /// 异步新增
        /// </summary>
        /// <param name="domainModel">领域模型(领域模型,命令模型,事件模型)</param>
        /// <returns></returns>
        Task AddAsync(UserDomainModel domainModel);

        /// <summary>
        /// 异步修改
        /// </summary>
        /// <param name="domainModel">领域模型(领域模型,命令模型,事件模型)</param>
        /// <returns></returns>
        Task UpdateAsync(UserDomainModel domainModel);

        /// <summary>
        /// 根据ID异步删除
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        Task DeleteByIdAsync(int id);

        /// <summary>
        /// 根据手机号异步查询
        /// </summary>
        /// <param name="mobile">手机号</param>
        /// <returns></returns>
        Task<UserDomainModel> GetByMobileAsync(string mobile);

        /// <summary>
        /// 根据用户名异步查询
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        Task<UserDomainModel> GetByUserNameAsync(string userName);
    }
}
