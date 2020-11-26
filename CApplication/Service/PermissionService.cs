using CApplication.IService;
using CApplication.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CApplication.Service
{
    /// <summary>
    /// 权限服务
    /// </summary>
    public class PermissionService : IPermissionService
    {
        public async Task<List<PermissionViewModel>> GetAllByUserIdAsync(int userId)
        {
            await Task.FromResult(false);
            return new List<PermissionViewModel>();
        }
    }
}
