using System;
using System.Collections.Generic;

namespace BambooChronoSyncUtility.DAL.EF.Model
{
    public partial class TimeReport
    {
        public int UserId { get; set; }
        public int TaskId { get; set; }
        public DateTime Date { get; set; }
        public double Hours { get; set; }
        public string? Status { get; set; }
        public int Type { get; set; }
        public bool IsSynchronized { get; set; }
        public int ProjectId { get; set; }
        public DateTime LastUpdated { get; set; }

        public virtual FieldInfo TypeNavigation { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
