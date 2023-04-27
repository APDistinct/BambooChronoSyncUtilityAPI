using System;
using System.Collections.Generic;

namespace BambooChronoSyncUtility.DAL.EF.Model
{
    public partial class OutProjectInfoPattern
    {
        public OutProjectInfoPattern()
        {
            OutTimeReportsInfos = new HashSet<OutTimeReportsInfo>();
        }

        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string? ProjectName { get; set; }
        public string DateName { get; set; } = null!;
        public string TimeName { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string WorkItemName { get; set; } = null!;

        public virtual Project Project { get; set; } = null!;
        public virtual ICollection<OutTimeReportsInfo> OutTimeReportsInfos { get; set; }
    }
}
