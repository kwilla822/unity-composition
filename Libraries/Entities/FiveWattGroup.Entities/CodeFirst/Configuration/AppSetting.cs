using System;

namespace FiveWattGroup.Entities.CodeFirst.Configuration
{
    public class AppSetting
    {
        public string ConfigKey { get; set; }
        public string ConfigValue { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset LastModifiedDate { get; set; }
        public string LastModifiedBy { get; set; }
    }
}