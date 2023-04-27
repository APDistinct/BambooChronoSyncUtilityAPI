using System;
using System.Collections.Generic;

namespace BambooChronoSyncUtility.DAL.EF.Model
{
    public partial class ProjectCollection
    {
        public ProjectCollection()
        {
            Projects = new HashSet<Project>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public int SubscriptionId { get; set; }
        public Guid TfsId { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
    }
}
