using System;
using System.Collections.Generic;

namespace BambooChronoSyncUtility.DAL.EF.Model
{
    public partial class LocationHistory
    {
        public int Id { get; set; }
        public int LocationId { get; set; }
        public int UserId { get; set; }
        public DateTime From { get; set; }
        public DateTime Till { get; set; }
    }
}
