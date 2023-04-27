using System;
using System.Collections.Generic;

namespace BambooChronoSyncUtility.DAL.EF.Model
{
    public partial class VirtualTaskAssignee
    {
        public int VirtualTaskId { get; set; }
        public int UserId { get; set; }

        public virtual VirtualTask VirtualTask { get; set; } = null!;
    }
}
