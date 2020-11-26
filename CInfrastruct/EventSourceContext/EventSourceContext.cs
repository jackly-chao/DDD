using CDomain.EventSource.Model;
using CInfrastruct.EventSourceMap;
using Microsoft.EntityFrameworkCore;

namespace CInfrastruct.EventSourceContext
{
    /// <summary>
    /// 事件回溯模型数据库上下文
    /// </summary>
    public class EventSourceContext : DbContext
    {
        public EventSourceContext(DbContextOptions<EventSourceContext> options) : base(options)
        {

        }

        #region 事件回溯模型集合
        /// <summary>
        /// 事件回溯模型集合
        /// </summary>
        public DbSet<EventSourceModel> EventSourceModels { get; set; }
        #endregion

        /// <summary>
        /// 配置事件溯源模型和数据模型的映射
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EventSourceModelMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
