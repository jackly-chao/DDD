using Microsoft.EntityFrameworkCore;

namespace BaseInfrastruct.Context
{
    /// <summary>
    /// 泛型数据库上下文基类
    /// </summary>
    /// <typeparam name="TContext">泛型数据库上下文</typeparam>
    public abstract class ContextCore<TContext> : DbContext where TContext : DbContext
    {
        public ContextCore(DbContextOptions<TContext> options) : base(options)
        {

        }
    }
}
