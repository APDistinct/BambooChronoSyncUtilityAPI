using System;
using System.Collections.Generic;

namespace BambooChronoSyncUtility.DAL.EF.Model
{
    public partial class ServerRoleName
    {
        public ServerRoleName()
        {
            Projects = new HashSet<VirtualProject>();
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<VirtualProject> Projects { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
