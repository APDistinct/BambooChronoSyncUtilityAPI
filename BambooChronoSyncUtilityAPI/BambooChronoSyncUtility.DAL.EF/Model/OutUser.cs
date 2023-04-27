using System;
using System.Collections.Generic;

namespace BambooChronoSyncUtility.DAL.EF.Model
{
    public partial class OutUser
    {
        public int UserId { get; set; }
        public string OutName { get; set; } = null!;

        public virtual User User { get; set; } = null!;
    }
}
