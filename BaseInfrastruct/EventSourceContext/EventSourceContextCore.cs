using Microsoft.EntityFrameworkCore;

namespace BaseInfrastruct.EventSourceContext
{
    /// <summary>
    /// 泛型事件回溯模型数据库上下文基类
    /// </summary>
    /// <typeparam name="TEventSourceContext">泛型事件回溯模型数据库上下文</typeparam>
    public abstract class EventSourceContextCore<TEventSourceContext> : DbContext where TEventSourceContext : DbContext
    {
        public EventSourceContextCore(DbContextOptions<TEventSourceContext> options) : base(options)
        {

        }
    }
}
