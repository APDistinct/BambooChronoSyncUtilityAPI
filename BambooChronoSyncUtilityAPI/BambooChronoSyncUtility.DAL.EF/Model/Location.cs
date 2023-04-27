using System;
using System.Collections.Generic;

namespace BambooChronoSyncUtility.DAL.EF.Model
{
    public partial class Location
    {
        public Location()
        {
            DateTransfers = new HashSet<DateTransfer>();
            Holidays = new HashSet<Holiday>();
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Type { get; set; }

        public virtual ICollection<DateTransfer> DateTransfers { get; set; }
        public virtual ICollection<Holiday> Holidays { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
