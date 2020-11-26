using CDomain.Model;
using CInfrastruct.Map;
using Microsoft.EntityFrameworkCore;

namespace CInfrastruct.Context
{
    /// <summary>
    /// 数据库上下文
    /// </summary>
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

        #region 领域模型集合
        /// <summary>
        /// 用户领域模型集合
        /// </summary>
        public DbSet<UserDomainModel> Users { get; set; }
        #endregion

        /// <summary>
        /// 配置领域模型->数据模型(数据表)映射
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            //modelBuilder.ApplyConfiguration(new CPermissionMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
