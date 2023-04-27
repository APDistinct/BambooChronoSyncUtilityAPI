using System;
using System.Collections.Generic;

namespace BambooChronoSyncUtility.DAL.EF.Model
{
    public partial class NotificationItem
    {
        public int Id { get; set; }
        public int RecipientId { get; set; }
        public int InitiatorId { get; set; }
        public int ProjectId { get; set; }
        public int NotificationType { get; set; }
        public int Attempt { get; set; }
        public string? Data { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ChangedDate { get; set; }
    }
}
