using System;
using System.Collections.Generic;

namespace BambooChronoSyncUtility.DAL.EF.Model
{
    public partial class FieldInfo
    {
        public FieldInfo()
        {
            SynchItems = new HashSet<SynchItem>();
            TimeReports = new HashSet<TimeReport>();
            VirtualTaskFieldValues = new HashSet<VirtualTaskFieldValue>();
            Types = new HashSet<WorkItemType>();
        }

        public int Id { get; set; }
        public string FieldName { get; set; } = null!;
        public string ReferenceName { get; set; } = null!;
        public string Alias { get; set; } = null!;
        public bool IsDefault { get; set; }
        public int FieldType { get; set; }
        public int EditType { get; set; }

        public virtual ICollection<SynchItem> SynchItems { get; set; }
        public virtual ICollection<TimeReport> TimeReports { get; set; }
        public virtual ICollection<VirtualTaskFieldValue> VirtualTaskFieldValues { get; set; }

        public virtual ICollection<WorkItemType> Types { get; set; }
    }
}
