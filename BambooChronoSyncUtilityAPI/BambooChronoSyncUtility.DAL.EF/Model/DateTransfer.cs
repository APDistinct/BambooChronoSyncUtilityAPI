using System;
using System.Collections.Generic;

namespace BambooChronoSyncUtility.DAL.EF.Model
{
    public partial class DateTransfer
    {
        public int Id { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Description { get; set; } = null!;
        public int LocationId { get; set; }
        public DateTime LastUpdated { get; set; }

        public virtual Location Location { get; set; } = null!;
    }
}
