using System;
using System.Collections.Generic;

namespace BambooChronoSyncUtility.DAL.EF.Model
{
    public partial class ActivePeriodList
    {
        public ActivePeriodList()
        {
            ActivePeriods = new HashSet<ActivePeriod>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime LastChanged { get; set; }
        public int WorkItemId { get; set; }
        public int ProjectId { get; set; }
        public int Revision { get; set; }

        public virtual Project Project { get; set; } = null!;
        public virtual ICollection<ActivePeriod> ActivePeriods { get; set; }
    }
}
