namespace CApplication.ViewModel
{
    /// <summary>
    /// 权限视图模型，视图模型可以理解成DTO
    /// </summary>
    public class PermissionViewModel : ViewModel
    {
        /// <summary>
        /// 权限ID
        /// </summary>
        public int PermissionId { get; set; }

        /// <summary>
        /// 权限名
        /// </summary>
        public string PermissionName { get; set; }

        /// <summary>
        /// 权限路径
        /// </summary>
        public string PermissionPath { get; set; }
    }
}
