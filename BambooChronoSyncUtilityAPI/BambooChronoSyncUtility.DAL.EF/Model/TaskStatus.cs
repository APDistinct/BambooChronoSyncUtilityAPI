using System;
using System.Collections.Generic;

namespace BambooChronoSyncUtility.DAL.EF.Model
{
    public partial class TaskStatus
    {
        public int TaskId { get; set; }
        public int UserId { get; set; }
        public DateTime StartDate { get; set; }
        public int ProjectId { get; set; }
        public string? Status { get; set; }
    }
}
