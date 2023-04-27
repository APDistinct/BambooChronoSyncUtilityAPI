using System;
using System.Collections.Generic;

namespace BambooChronoSyncUtility.DAL.EF.Model
{
    public partial class SynchItem
    {
        public int Id { get; set; }
        public double OldHours { get; set; }
        public int Attempt { get; set; }
        public string Editor { get; set; } = null!;
        public int UserId { get; set; }
        public int TaskId { get; set; }
        public int ProjectId { get; set; }
        public DateTime Date { get; set; }
        public int FieldId { get; set; }
        public DateTime LastUpdate { get; set; }
        public double Hours { get; set; }
        public int? Revision { get; set; }

        public virtual FieldInfo Field { get; set; } = null!;
    }
}
