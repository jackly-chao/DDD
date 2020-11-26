using BaseDomain.EventSource.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaseInfrastruct.EventSourceMap
{
    /// <summary>
    /// 泛型事件溯源模型->数据模型映射基类
    /// </summary>
    /// <typeparam name="TEventSourceModel">泛型事件溯源模型</typeparam>
    public abstract class EventSourceModelMapCore<TEventSourceModel> : IEntityTypeConfiguration<TEventSourceModel> where TEventSourceModel : EventSourceModelCore<object, object>
    {
        public abstract void Configure(EntityTypeBuilder<TEventSourceModel> builder);
    }
}
