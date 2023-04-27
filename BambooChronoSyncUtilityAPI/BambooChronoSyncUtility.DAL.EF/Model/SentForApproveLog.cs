using System;
using System.Collections.Generic;

namespace BambooChronoSyncUtility.DAL.EF.Model
{
    public partial class SentForApproveLog
    {
        public int UserId { get; set; }
        public DateTime Date { get; set; }
    }
}
