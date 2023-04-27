using System;
using System.Collections.Generic;

namespace BambooChronoSyncUtility.DAL.EF.Model
{
    public partial class VirtualTask
    {
        public VirtualTask()
        {
            VirtualTaskAssignees = new HashSet<VirtualTaskAssignee>();
            VirtualTaskAssigneesRoles = new HashSet<VirtualTaskAssigneesRole>();
            VirtualTaskFieldValues = new HashSet<VirtualTaskFieldValue>();
        }

        public int Id { get; set; }
        public int ProjectId { get; set; }
        public bool? IsAllUsersIncluded { get; set; }

        public virtual VirtualProject Project { get; set; } = null!;
        public virtual ICollection<VirtualTaskAssignee> VirtualTaskAssignees { get; set; }
        public virtual ICollection<VirtualTaskAssigneesRole> VirtualTaskAssigneesRoles { get; set; }
        public virtual ICollection<VirtualTaskFieldValue> VirtualTaskFieldValues { get; set; }
    }
}
