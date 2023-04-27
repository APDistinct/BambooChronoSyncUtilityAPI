using System;
using System.Collections.Generic;

namespace BambooChronoSyncUtility.DAL.EF.Model
{
    public partial class Holiday
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string? Description { get; set; }
        public int LocationId { get; set; }
        public int EditorId { get; set; }
        public DateTime LastUpdated { get; set; }

        public virtual User Editor { get; set; } = null!;
        public virtual Location Location { get; set; } = null!;
    }
}
