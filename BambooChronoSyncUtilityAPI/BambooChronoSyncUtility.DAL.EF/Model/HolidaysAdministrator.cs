using System;
using System.Collections.Generic;

namespace BambooChronoSyncUtility.DAL.EF.Model
{
    public partial class HolidaysAdministrator
    {
        public int UserId { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
