using System;
using System.Collections.Generic;

namespace BambooChronoSyncUtility.DAL.EF.Model
{
    public partial class ActivePeriod
    {
        public int Id { get; set; }
        public int ActivePeriodListId { get; set; }
        public DateTime Begin { get; set; }
        public DateTime End { get; set; }

        public virtual ActivePeriodList ActivePeriodList { get; set; } = null!;
    }
}
