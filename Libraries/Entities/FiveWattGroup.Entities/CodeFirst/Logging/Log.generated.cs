// ReSharper disable RedundantUsingDirective
// ReSharper disable DoNotCallOverridableMethodsInConstructor
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable RedundantNameQualifier

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FiveWattGroup.Entities.CodeFirst.Logging
{
    // Log
    public partial class Log
    {
        public int Id { get; set; } // Id (Primary key)
        public int LogLevelId { get; set; } // LogLevelId
        public string ShortMessage { get; set; } // ShortMessage
        public string FullMessage { get; set; } // FullMessage
        public string IpAddress { get; set; } // IpAddress
        public int? CustomerId { get; set; } // CustomerId
        public string PageUrl { get; set; } // PageUrl
        public string ReferrerUrl { get; set; } // ReferrerUrl
        public DateTime CreatedOnUtc { get; set; } // CreatedOnUtc
    }

}
