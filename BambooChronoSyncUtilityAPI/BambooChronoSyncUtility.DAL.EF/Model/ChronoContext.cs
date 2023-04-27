using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BambooChronoSyncUtility.DAL.EF.Model
{
    public partial class ChronoContext : DbContext
    {
        public ChronoContext()
        {
        }

        public ChronoContext(DbContextOptions<ChronoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ActivePeriod> ActivePeriods { get; set; } = null!;
        public virtual DbSet<ActivePeriodList> ActivePeriodLists { get; set; } = null!;
        public virtual DbSet<Administrator> Administrators { get; set; } = null!;
        public virtual DbSet<DateTransfer> DateTransfers { get; set; } = null!;
        public virtual DbSet<DefaultTimetable> DefaultTimetables { get; set; } = null!;
        public virtual DbSet<FieldInfo> FieldInfos { get; set; } = null!;
        public virtual DbSet<Holiday> Holidays { get; set; } = null!;
        public virtual DbSet<HolidaysAdministrator> HolidaysAdministrators { get; set; } = null!;
        public virtual DbSet<License> Licenses { get; set; } = null!;
        public virtual DbSet<Location> Locations { get; set; } = null!;
        public virtual DbSet<LocationHistory> LocationHistories { get; set; } = null!;
        public virtual DbSet<NotificationItem> NotificationItems { get; set; } = null!;
        public virtual DbSet<OutProjectInfoPattern> OutProjectInfoPatterns { get; set; } = null!;
        public virtual DbSet<OutTimeReportsDatum> OutTimeReportsData { get; set; } = null!;
        public virtual DbSet<OutTimeReportsInfo> OutTimeReportsInfos { get; set; } = null!;
        public virtual DbSet<OutUser> OutUsers { get; set; } = null!;
        public virtual DbSet<OutWorkItem> OutWorkItems { get; set; } = null!;
        public virtual DbSet<Project> Projects { get; set; } = null!;
        public virtual DbSet<ProjectCollection> ProjectCollections { get; set; } = null!;
        public virtual DbSet<ProjectRole> ProjectRoles { get; set; } = null!;
        public virtual DbSet<RoleMember> RoleMembers { get; set; } = null!;
        public virtual DbSet<SentForApproveLog> SentForApproveLogs { get; set; } = null!;
        public virtual DbSet<ServerRoleName> ServerRoleNames { get; set; } = null!;
        public virtual DbSet<Setting> Settings { get; set; } = null!;
        public virtual DbSet<SynchItem> SynchItems { get; set; } = null!;
        public virtual DbSet<TaskStatus> TaskStatuses { get; set; } = null!;
        public virtual DbSet<TimeReport> TimeReports { get; set; } = null!;
        public virtual DbSet<Timetable> Timetables { get; set; } = null!;
        public virtual DbSet<TimetableHistory> TimetableHistories { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<VirtualProject> VirtualProjects { get; set; } = null!;
        public virtual DbSet<VirtualTask> VirtualTasks { get; set; } = null!;
        public virtual DbSet<VirtualTaskAssignee> VirtualTaskAssignees { get; set; } = null!;
        public virtual DbSet<VirtualTaskAssigneesRole> VirtualTaskAssigneesRoles { get; set; } = null!;
        public virtual DbSet<VirtualTaskFieldValue> VirtualTaskFieldValues { get; set; } = null!;
        public virtual DbSet<WiactiveWeek> WiactiveWeeks { get; set; } = null!;
        public virtual DbSet<WorkItemInfo> WorkItemInfos { get; set; } = null!;
        public virtual DbSet<WorkItemType> WorkItemTypes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Server=APD\\APDSERVER;User Id=txchrono;Password=Tx12cHrono34;Database=TimeTrack");
//            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<ActivePeriod>(entity =>
            {
                entity.ToTable("ActivePeriod", "dbo");

                entity.HasIndex(e => e.ActivePeriodListId, "IX_ActivePeriod");

                entity.Property(e => e.Begin).HasColumnType("datetime");

                entity.Property(e => e.End).HasColumnType("datetime");

                entity.HasOne(d => d.ActivePeriodList)
                    .WithMany(p => p.ActivePeriods)
                    .HasForeignKey(d => d.ActivePeriodListId)
                    .HasConstraintName("FK_ActivePeriod_ActivePeriodList");
            });

            modelBuilder.Entity<ActivePeriodList>(entity =>
            {
                entity.ToTable("ActivePeriodList", "dbo");

                entity.HasIndex(e => new { e.UserId, e.WorkItemId, e.ProjectId }, "IX_ActivePeriodList")
                    .IsUnique();

                entity.Property(e => e.LastChanged).HasColumnType("datetime");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ActivePeriodLists)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ActivePeriodList_Project");
            });

            modelBuilder.Entity<Administrator>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__Administrators__656C112C");

                entity.ToTable("Administrators", "dbo");

                entity.Property(e => e.UserId)
                    .ValueGeneratedNever()
                    .HasColumnName("userId");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Administrator)
                    .HasForeignKey<Administrator>(d => d.UserId)
                    .HasConstraintName("FK__Administr__userI__66603565");
            });

            modelBuilder.Entity<DateTransfer>(entity =>
            {
                entity.ToTable("DateTransfer", "dbo");

                entity.Property(e => e.FromDate).HasColumnType("datetime");

                entity.Property(e => e.LastUpdated).HasColumnType("datetime");

                entity.Property(e => e.ToDate).HasColumnType("datetime");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.DateTransfers)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DateTrans__locat__2A164134");
            });

            modelBuilder.Entity<DefaultTimetable>(entity =>
            {
                entity.ToTable("DefaultTimetable", "dbo");

                entity.Property(e => e.LastUpdated).HasColumnType("datetime");

                entity.Property(e => e.ValidFrom).HasColumnType("datetime");
            });

            modelBuilder.Entity<FieldInfo>(entity =>
            {
                entity.ToTable("FieldInfo", "dbo");

                entity.Property(e => e.ReferenceName).HasMaxLength(300);
            });

            modelBuilder.Entity<Holiday>(entity =>
            {
                entity.ToTable("Holidays", "dbo");

                entity.HasIndex(e => new { e.Date, e.LocationId }, "IX_Holidays")
                    .IsUnique();

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.LastUpdated).HasColumnType("datetime");

                entity.HasOne(d => d.Editor)
                    .WithMany(p => p.Holidays)
                    .HasForeignKey(d => d.EditorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Holidays_User");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Holidays)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK_Holidays_Locations");
            });

            modelBuilder.Entity<HolidaysAdministrator>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("HolidaysAdministrators", "dbo");

                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.HasOne(d => d.User)
                    .WithOne(p => p.HolidaysAdministrator)
                    .HasForeignKey<HolidaysAdministrator>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HolidaysAdministrators_User");
            });

            modelBuilder.Entity<License>(entity =>
            {
                entity.ToTable("License", "dbo");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("Locations", "dbo");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<LocationHistory>(entity =>
            {
                entity.ToTable("LocationHistory", "dbo");

                entity.Property(e => e.From).HasColumnType("datetime");

                entity.Property(e => e.Till).HasColumnType("datetime");
            });

            modelBuilder.Entity<NotificationItem>(entity =>
            {
                entity.ToTable("NotificationItem", "dbo");

                entity.Property(e => e.ChangedDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<OutProjectInfoPattern>(entity =>
            {
                entity.ToTable("OutProjectInfoPattern", "dbo");

                entity.Property(e => e.DateName).HasMaxLength(255);

                entity.Property(e => e.ProjectName).HasMaxLength(255);

                entity.Property(e => e.TimeName).HasMaxLength(255);

                entity.Property(e => e.UserName).HasMaxLength(255);

                entity.Property(e => e.WorkItemName).HasMaxLength(255);

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.OutProjectInfoPatterns)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OutProjectInfoPattern_ProjectId__Project_Id");
            });

            modelBuilder.Entity<OutTimeReportsDatum>(entity =>
            {
                entity.ToTable("OutTimeReportsData", "dbo");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DateValue)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.UserValue).HasMaxLength(255);

                entity.Property(e => e.WorkItemValue).HasMaxLength(255);

                entity.HasOne(d => d.OutTimeReportsInfo)
                    .WithMany(p => p.OutTimeReportsData)
                    .HasForeignKey(d => d.OutTimeReportsInfoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OutTimeReportsData_OutTimeReportsInfoId__OutTimeReportsInfo_Id");
            });

            modelBuilder.Entity<OutTimeReportsInfo>(entity =>
            {
                entity.ToTable("OutTimeReportsInfo", "dbo");

                entity.Property(e => e.FileName).HasMaxLength(255);

                entity.Property(e => e.LoadDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.OutProjectInfoPattern)
                    .WithMany(p => p.OutTimeReportsInfos)
                    .HasForeignKey(d => d.OutProjectInfoPatternId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OutTimeReportsInfo_OutProjectInfoPatternId__OutProjectInfoPattern_Id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.OutTimeReportsInfos)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OutTimeReportsInfo_UserId__User_UserId");
            });

            modelBuilder.Entity<OutUser>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__OutUser__Id");

                entity.ToTable("OutUser", "dbo");

                entity.HasIndex(e => e.OutName, "UNQ__OutUser__OutName")
                    .IsUnique();

                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.Property(e => e.OutName).HasMaxLength(255);

                entity.HasOne(d => d.User)
                    .WithOne(p => p.OutUser)
                    .HasForeignKey<OutUser>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OutUser_UserId__User_UserId");
            });

            modelBuilder.Entity<OutWorkItem>(entity =>
            {
                entity.ToTable("OutWorkItem", "dbo");

                entity.Property(e => e.OutName).HasMaxLength(255);

                entity.HasOne(d => d.WorkItemInfo)
                    .WithMany(p => p.OutWorkItems)
                    .HasForeignKey(d => new { d.ProjectId, d.WorkItemId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OutWorkItem_WorkItemId__WorkItemInfo_WorkItemId");
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.ToTable("Project", "dbo");

                entity.Property(e => e.Name)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Uri)
                    .HasMaxLength(8000)
                    .IsUnicode(false);

                entity.HasOne(d => d.ProjectCollection)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.ProjectCollectionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Project_ProjectCollection");
            });

            modelBuilder.Entity<ProjectCollection>(entity =>
            {
                entity.ToTable("ProjectCollection", "dbo");

                entity.Property(e => e.Name)
                    .HasMaxLength(1000)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ProjectRole>(entity =>
            {
                entity.ToTable("ProjectRoles", "dbo");

                entity.Property(e => e.Name)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_ProjectRoles_ProjectRoles");
            });

            modelBuilder.Entity<RoleMember>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.ProjectId, e.RoleId });

                entity.ToTable("RoleMember", "dbo");

                entity.HasIndex(e => new { e.ProjectId, e.RoleId }, "IX_RoleMember_missing_137");

                entity.HasIndex(e => new { e.UserId, e.RoleId }, "IX_RoleMember_missing_1883");

                entity.HasIndex(e => e.ProjectId, "IX_RoleMember_missing_205");

                entity.HasIndex(e => e.ProjectId, "IX_RoleMember_missing_207");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.RoleMembers)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Member_Project");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RoleMembers)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Member_ProjectRoles");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RoleMembers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Member_User");
            });

            modelBuilder.Entity<SentForApproveLog>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.Date })
                    .HasName("PK");

                entity.ToTable("SentForApproveLog", "dbo");

                entity.Property(e => e.Date).HasColumnType("datetime");
            });

            modelBuilder.Entity<ServerRoleName>(entity =>
            {
                entity.ToTable("ServerRoleNames", "dbo");

                entity.HasMany(d => d.Projects)
                    .WithMany(p => p.ServerRoles)
                    .UsingEntity<Dictionary<string, object>>(
                        "VirtualResponsibleRole",
                        l => l.HasOne<VirtualProject>().WithMany().HasForeignKey("ProjectId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_VirtualResponsibleRoles_ToVirtualProjects"),
                        r => r.HasOne<ServerRoleName>().WithMany().HasForeignKey("ServerRoleId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_VirtualResponsibleRoles_ToServerRoleNames"),
                        j =>
                        {
                            j.HasKey("ServerRoleId", "ProjectId").HasName("PK__VirtualR__E58453EE8E562F4E");

                            j.ToTable("VirtualResponsibleRoles", "dbo");
                        });

                entity.HasMany(d => d.Users)
                    .WithMany(p => p.ServerRoles)
                    .UsingEntity<Dictionary<string, object>>(
                        "ServerRoleMember",
                        l => l.HasOne<User>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__ServerRol__userI__05D8E0BE"),
                        r => r.HasOne<ServerRoleName>().WithMany().HasForeignKey("ServerRoleId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__ServerRol__serve__04E4BC85"),
                        j =>
                        {
                            j.HasKey("ServerRoleId", "UserId");

                            j.ToTable("ServerRoleMembers", "dbo");
                        });
            });

            modelBuilder.Entity<Setting>(entity =>
            {
                entity.ToTable("Settings", "dbo");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Key).HasMaxLength(255);

                entity.Property(e => e.Value).HasMaxLength(1000);
            });

            modelBuilder.Entity<SynchItem>(entity =>
            {
                entity.ToTable("SynchItem", "dbo");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Editor).HasMaxLength(255);

                entity.Property(e => e.LastUpdate).HasColumnType("datetime");

                entity.HasOne(d => d.Field)
                    .WithMany(p => p.SynchItems)
                    .HasForeignKey(d => d.FieldId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SynchItem_ToFieldInfo");
            });

            modelBuilder.Entity<TaskStatus>(entity =>
            {
                entity.HasKey(e => new { e.TaskId, e.UserId, e.StartDate, e.ProjectId })
                    .HasName("PK1");

                entity.ToTable("TaskStatuses", "dbo");

                entity.HasIndex(e => new { e.UserId, e.StartDate, e.ProjectId }, "idx_TaskStatuses_UserId_StartDate_ProjectId");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.Status)
                    .HasMaxLength(1000)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TimeReport>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.TaskId, e.Date, e.Type, e.ProjectId });

                entity.ToTable("TimeReports", "dbo");

                entity.HasIndex(e => new { e.ProjectId, e.Date }, "idx_TimeReports_ProjectId_Date");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.LastUpdated).HasColumnType("datetime");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.TypeNavigation)
                    .WithMany(p => p.TimeReports)
                    .HasForeignKey(d => d.Type)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TimeReports_WorkItemTypes");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TimeReports)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_TimeReports_User");
            });

            modelBuilder.Entity<Timetable>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__Timetable__14270015");

                entity.ToTable("Timetable", "dbo");

                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.Property(e => e.LastUpdated).HasColumnType("datetime");

                entity.Property(e => e.ValidFrom).HasColumnType("datetime");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Timetable)
                    .HasForeignKey<Timetable>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Timetable__userI__151B244E");
            });

            modelBuilder.Entity<TimetableHistory>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.ValidFrom });

                entity.ToTable("TimetableHistory", "dbo");

                entity.Property(e => e.ValidFrom).HasColumnType("datetime");

                entity.Property(e => e.LastUpdated).HasColumnType("datetime");

                entity.Property(e => e.ValidTill).HasColumnType("datetime");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TimetableHistories)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Timetable__userI__1BC821DD");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User", "dbo");

                entity.HasIndex(e => new { e.DisplayName, e.Deleted }, "IX_User_missing_119");

                entity.HasIndex(e => new { e.Sid, e.Deleted }, "IX_User_missing_126");

                entity.HasIndex(e => new { e.UserName, e.Deleted }, "IX_User_missing_77");

                entity.Property(e => e.Department).HasMaxLength(255);

                entity.Property(e => e.DisplayName).HasMaxLength(255);

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.LocationFrom).HasColumnType("datetime");

                entity.Property(e => e.Position).HasMaxLength(255);

                entity.Property(e => e.Sid).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(255);

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Locations");
            });

            modelBuilder.Entity<VirtualProject>(entity =>
            {
                entity.ToTable("VirtualProjects", "dbo");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasMany(d => d.Users)
                    .WithMany(p => p.Projects)
                    .UsingEntity<Dictionary<string, object>>(
                        "VirtualResponsiblePerson",
                        l => l.HasOne<User>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_VirtualResponsiblePersons_ToUser"),
                        r => r.HasOne<VirtualProject>().WithMany().HasForeignKey("ProjectId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_VirtualResponsiblePersons_ToVirtualProjects"),
                        j =>
                        {
                            j.HasKey("ProjectId", "UserId").HasName("PK__VirtualR__A76232344F71CBE9");

                            j.ToTable("VirtualResponsiblePersons", "dbo");
                        });
            });

            modelBuilder.Entity<VirtualTask>(entity =>
            {
                entity.ToTable("VirtualTasks", "dbo");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.IsAllUsersIncluded)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.VirtualTasks)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VirtualTasks_ToVirtualProjects");
            });

            modelBuilder.Entity<VirtualTaskAssignee>(entity =>
            {
                entity.HasKey(e => new { e.VirtualTaskId, e.UserId })
                    .HasName("PK__VirtualT__61F3D8E893148B37");

                entity.ToTable("VirtualTaskAssignees", "dbo");

                entity.HasOne(d => d.VirtualTask)
                    .WithMany(p => p.VirtualTaskAssignees)
                    .HasForeignKey(d => d.VirtualTaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VirtualTaskAssignees_ToVirtualTasks");
            });

            modelBuilder.Entity<VirtualTaskAssigneesRole>(entity =>
            {
                entity.HasKey(e => new { e.VirtualTaskId, e.RoleId })
                    .HasName("PK__VirtualT__0824F8CD644A3B56");

                entity.ToTable("VirtualTaskAssigneesRoles", "dbo");

                entity.HasOne(d => d.VirtualTask)
                    .WithMany(p => p.VirtualTaskAssigneesRoles)
                    .HasForeignKey(d => d.VirtualTaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VirtualTaskAssigneesRoles_ToVirtualTasks");
            });

            modelBuilder.Entity<VirtualTaskFieldValue>(entity =>
            {
                entity.HasKey(e => new { e.FieldId, e.TaskId })
                    .HasName("PK__VirtualT__CF706B9C6E6C2A6F");

                entity.ToTable("VirtualTaskFieldValues", "dbo");

                entity.Property(e => e.Value).HasMaxLength(500);

                entity.HasOne(d => d.Field)
                    .WithMany(p => p.VirtualTaskFieldValues)
                    .HasForeignKey(d => d.FieldId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VirtualTaskFieldsValues_ToFieldInfo");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.VirtualTaskFieldValues)
                    .HasForeignKey(d => d.TaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VirtualTaskFieldValues_ToVirtualTasks");
            });

            modelBuilder.Entity<WiactiveWeek>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.TaskId, e.ProjectId, e.StartDate })
                    .IsClustered(false);

                entity.ToTable("WIActiveWeek", "dbo");

                entity.HasIndex(e => e.UserId, "IX_WIActiveWeek")
                    .IsClustered();

                entity.Property(e => e.StartDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<WorkItemInfo>(entity =>
            {
                entity.HasKey(e => new { e.ProjectId, e.WorkItemId });

                entity.ToTable("WorkItemInfo", "dbo");

                entity.Property(e => e.WorkItemType)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<WorkItemType>(entity =>
            {
                entity.ToTable("WorkItemTypes", "dbo");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.HasMany(d => d.Fields)
                    .WithMany(p => p.Types)
                    .UsingEntity<Dictionary<string, object>>(
                        "WorkItemTypeField",
                        l => l.HasOne<FieldInfo>().WithMany().HasForeignKey("FieldId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_WorkItemTypeFields_ToFieldInfo"),
                        r => r.HasOne<WorkItemType>().WithMany().HasForeignKey("TypeId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_WorkItemTypeFields_ToWorkItemTypes"),
                        j =>
                        {
                            j.HasKey("TypeId", "FieldId");

                            j.ToTable("WorkItemTypeFields", "dbo");
                        });
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
