using System;
using System.Collections.Generic;

namespace BambooChronoSyncUtility.DAL.EF.Model
{
    public partial class User
    {
        public User()
        {
            Holidays = new HashSet<Holiday>();
            OutTimeReportsInfos = new HashSet<OutTimeReportsInfo>();
            RoleMembers = new HashSet<RoleMember>();
            TimeReports = new HashSet<TimeReport>();
            TimetableHistories = new HashSet<TimetableHistory>();
            Projects = new HashSet<VirtualProject>();
            ServerRoles = new HashSet<ServerRoleName>();
        }

        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? DisplayName { get; set; }
        public string? Position { get; set; }
        public string? Department { get; set; }
        public int LocationId { get; set; }
        public DateTime LocationFrom { get; set; }
        public string Sid { get; set; } = null!;
        public bool Deleted { get; set; }

        public virtual Location Location { get; set; } = null!;
        public virtual Administrator? Administrator { get; set; }
        public virtual HolidaysAdministrator? HolidaysAdministrator { get; set; }
        public virtual OutUser? OutUser { get; set; }
        public virtual Timetable? Timetable { get; set; }
        public virtual ICollection<Holiday> Holidays { get; set; }
        public virtual ICollection<OutTimeReportsInfo> OutTimeReportsInfos { get; set; }
        public virtual ICollection<RoleMember> RoleMembers { get; set; }
        public virtual ICollection<TimeReport> TimeReports { get; set; }
        public virtual ICollection<TimetableHistory> TimetableHistories { get; set; }

        public virtual ICollection<VirtualProject> Projects { get; set; }
        public virtual ICollection<ServerRoleName> ServerRoles { get; set; }
    }
}
