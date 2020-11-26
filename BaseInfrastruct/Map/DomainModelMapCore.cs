using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaseInfrastruct.Map
{
    /// <summary>
    /// 泛型领域模型->数据模型映射基类
    /// </summary>
    /// <typeparam name="TDomainModel">泛型领域模型</typeparam>
    public abstract class DomainModelMapCore<TDomainModel> : IEntityTypeConfiguration<TDomainModel> where TDomainModel : class
    {
        public abstract void Configure(EntityTypeBuilder<TDomainModel> builder);
    }
}
