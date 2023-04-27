using System;
using System.Collections.Generic;

namespace BambooChronoSyncUtility.DAL.EF.Model
{
    public partial class Project
    {
        public Project()
        {
            ActivePeriodLists = new HashSet<ActivePeriodList>();
            OutProjectInfoPatterns = new HashSet<OutProjectInfoPattern>();
            RoleMembers = new HashSet<RoleMember>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Uri { get; set; } = null!;
        public int ProjectCollectionId { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ProjectCollection ProjectCollection { get; set; } = null!;
        public virtual ICollection<ActivePeriodList> ActivePeriodLists { get; set; }
        public virtual ICollection<OutProjectInfoPattern> OutProjectInfoPatterns { get; set; }
        public virtual ICollection<RoleMember> RoleMembers { get; set; }
    }
}
