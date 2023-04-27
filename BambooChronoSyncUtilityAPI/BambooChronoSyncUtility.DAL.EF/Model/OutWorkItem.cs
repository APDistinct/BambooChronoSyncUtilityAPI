using System;
using System.Collections.Generic;

namespace BambooChronoSyncUtility.DAL.EF.Model
{
    public partial class OutWorkItem
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int WorkItemId { get; set; }
        public string OutName { get; set; } = null!;

        public virtual WorkItemInfo WorkItemInfo { get; set; } = null!;
    }
}
