// ReSharper disable RedundantUsingDirective
// ReSharper disable DoNotCallOverridableMethodsInConstructor
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable RedundantNameQualifier

using System;
using System.Data.Entity.ModelConfiguration;
using FiveWattGroup.Entities.CodeFirst.Logging;
   
using DatabaseGeneratedOption = System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption;

namespace FiveWattGroup.DataAccess.CodeFirst.TypeConfigurations.Logging
{
    // Log
    internal partial class LogTypeConfiguration : EntityTypeConfiguration<Log>
    {
        public LogTypeConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".Log");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.LogLevelId).HasColumnName("LogLevelId").IsRequired();
            Property(x => x.ShortMessage).HasColumnName("ShortMessage").IsRequired();
            Property(x => x.FullMessage).HasColumnName("FullMessage").IsOptional();
            Property(x => x.IpAddress).HasColumnName("IpAddress").IsOptional().HasMaxLength(200);
            Property(x => x.CustomerId).HasColumnName("CustomerId").IsOptional();
            Property(x => x.PageUrl).HasColumnName("PageUrl").IsOptional();
            Property(x => x.ReferrerUrl).HasColumnName("ReferrerUrl").IsOptional();
            Property(x => x.CreatedOnUtc).HasColumnName("CreatedOnUtc").IsRequired();
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
