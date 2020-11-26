using CApplication.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CApplication.IService
{
    /// <summary>
    /// 权限服务接口
    /// </summary>
    public interface IPermissionService 
    {
        /// <summary>
        /// 根据用户ID异步查询列表
        /// </summary>
        /// <returns></returns>
        Task<List<PermissionViewModel>> GetAllByUserIdAsync(int userId);
    }
}
