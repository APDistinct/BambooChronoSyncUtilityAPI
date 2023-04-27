using System;
using System.Collections.Generic;

namespace BambooChronoSyncUtility.DAL.EF.Model
{
    public partial class WorkItemInfo
    {
        public WorkItemInfo()
        {
            OutWorkItems = new HashSet<OutWorkItem>();
        }

        public int ProjectId { get; set; }
        public int WorkItemId { get; set; }
        public string? WorkItemType { get; set; }

        public virtual ICollection<OutWorkItem> OutWorkItems { get; set; }
    }
}
