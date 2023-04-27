using System;
using System.Collections.Generic;

namespace BambooChronoSyncUtility.DAL.EF.Model
{
    public partial class OutTimeReportsDatum
    {
        public int Id { get; set; }
        public int OutTimeReportsInfoId { get; set; }
        public DateTime DateValue { get; set; }
        public double TimeValue { get; set; }
        public string UserValue { get; set; } = null!;
        public string WorkItemValue { get; set; } = null!;
        public int? Mark { get; set; }

        public virtual OutTimeReportsInfo OutTimeReportsInfo { get; set; } = null!;
    }
}
