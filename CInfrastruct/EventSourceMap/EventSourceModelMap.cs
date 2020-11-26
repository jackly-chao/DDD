using CDomain.EventSource.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CInfrastruct.EventSourceMap
{
    /// <summary>
    /// 事件溯源模型->数据模型映射
    /// </summary>
    public class EventSourceModelMap : IEntityTypeConfiguration<EventSourceModel>
    {
        public void Configure(EntityTypeBuilder<EventSourceModel> builder)
        {
            builder.Property(c => c.Timestamp)
                  .HasColumnName("CreationTime");

            builder.Property(c => c.MessageType)
               .HasColumnName("Action");
        }
    }
}
