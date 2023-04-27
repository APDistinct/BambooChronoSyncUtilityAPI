using System;
using System.Collections.Generic;

namespace BambooChronoSyncUtility.DAL.EF.Model
{
    public partial class WorkItemType
    {
        public WorkItemType()
        {
            Fields = new HashSet<FieldInfo>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<FieldInfo> Fields { get; set; }
    }
}
