using CDomain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CInfrastruct.Map
{
    /// <summary>
    /// 用户领域模型->数据模型(数据表)映射
    /// </summary>
    public class UserMap : IEntityTypeConfiguration<UserDomainModel>
    {
        public void Configure(EntityTypeBuilder<UserDomainModel> builder)
        {
            builder.ToTable("CMembers");

            builder.Property(d => d.Id)
                 .HasColumnName("Id");
            builder.Property(d => d.Mobile)
                .HasColumnName("Tel");
        }
    }
}
