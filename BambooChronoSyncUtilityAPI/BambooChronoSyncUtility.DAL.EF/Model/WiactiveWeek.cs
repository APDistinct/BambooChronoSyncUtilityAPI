using System;
using System.Collections.Generic;

namespace BambooChronoSyncUtility.DAL.EF.Model
{
    public partial class WiactiveWeek
    {
        public int UserId { get; set; }
        public int TaskId { get; set; }
        public int ProjectId { get; set; }
        public DateTime StartDate { get; set; }
    }
}
