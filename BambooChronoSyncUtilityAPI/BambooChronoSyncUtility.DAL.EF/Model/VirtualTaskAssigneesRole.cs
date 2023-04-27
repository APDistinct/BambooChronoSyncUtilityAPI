using System;
using System.Collections.Generic;

namespace BambooChronoSyncUtility.DAL.EF.Model
{
    public partial class VirtualTaskAssigneesRole
    {
        public int VirtualTaskId { get; set; }
        public int RoleId { get; set; }

        public virtual VirtualTask VirtualTask { get; set; } = null!;
    }
}
