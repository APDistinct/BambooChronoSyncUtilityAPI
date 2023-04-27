using System;
using System.Collections.Generic;

namespace BambooChronoSyncUtility.DAL.EF.Model
{
    public partial class RoleMember
    {
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public int RoleId { get; set; }

        public virtual Project Project { get; set; } = null!;
        public virtual ProjectRole Role { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
