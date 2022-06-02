using System.Data.Entity.ModelConfiguration;

using FiveWattGroup.Entities.CodeFirst.Configuration;

namespace FiveWattGroup.DataAccess.CodeFirst.TypeConfigurations.Configuration
{
    public class AppSettingTypeConfiguration : EntityTypeConfiguration<AppSetting>
    {
        public AppSettingTypeConfiguration()
        {
            ToTable("dbo.AppSetting");
            HasKey(x => x.ConfigKey);
            Property(x => x.ConfigKey).HasColumnName("ConfigKey").IsRequired();
            Property(x => x.ConfigValue).HasColumnName("ConfigValue").IsRequired().IsUnicode(false).HasMaxLength(255);
            Property(x => x.ConfigValue).HasColumnName("Category").IsRequired().IsUnicode(false).HasMaxLength(50);
            Property(x => x.ConfigValue).HasColumnName("SubCategory").IsRequired().IsUnicode(false).HasMaxLength(50);
            Property(x => x.CreatedDate)
                .HasColumnName("CreatedDate")
                .IsRequired()
                .HasColumnType("datetimeoffset");
            Property(x => x.LastModifiedDate)
                .HasColumnName("LastModifiedDate")
                .IsRequired();
            Property(x => x.LastModifiedBy)
                .HasColumnName("LastModifiedBy")
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(30);
        }
    }
}