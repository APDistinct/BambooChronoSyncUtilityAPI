using System;
using System.Collections.Generic;

namespace BambooChronoSyncUtility.DAL.EF.Model
{
    public partial class VirtualProject
    {
        public VirtualProject()
        {
            VirtualTasks = new HashSet<VirtualTask>();
            ServerRoles = new HashSet<ServerRoleName>();
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<VirtualTask> VirtualTasks { get; set; }

        public virtual ICollection<ServerRoleName> ServerRoles { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
