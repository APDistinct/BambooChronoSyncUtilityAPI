using BambooChronoSyncUtility.Application;
using BambooChronoSyncUtility.Application.Models;
using BambooChronoSyncUtility.DAL.EF.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BambooChronoSyncUtility.Service.Repositories
{
    public interface IChronoRepository
    {
        Task<Dictionary<TimeDictionary, double>> GetTimeOff(int Id, DateTime start, DateTime end);
        Task<Dictionary<TimeDictionary, string>> GetStatus(int Id, DateTime start, DateTime end);
        Task GetChronoUserIds(IEnumerable<IUserIdChrono> idChronos);
        Task<int> SaveDaysOff(TimeOffModel timeOffModel);
        Task<string> Test();
    }
    public class ChronoRepository : IChronoRepository
    {
        private readonly int TimeOffId = -1; //900;
        private readonly ChronoContext _context;
        private readonly DAL.EF.Model.FieldInfo fieldInfo;

        public ChronoRepository(ChronoContext context)
        {
            _context = context;
            fieldInfo =  _context.FieldInfos.First(t => t.Id == 0);
        }
        
        public async Task<Dictionary<TimeDictionary, double>> GetTimeOff(int Id, DateTime start, DateTime end)
        {
            var q = _context.TimeReports.Where(tr => tr.UserId == Id && tr.ProjectId == TimeOffId && tr.Date <= end && tr.Date >= start);
            //var q = from tr in _context.TimeReports where tr.UserId == Id && tr.ProjectId == TimeOffId && tr.Date <= end && tr.Date >= start
            //        join ts in _context.TaskStatuses
            //            on new { tr.ProjectId, tr.UserId, Date = GetMonday(tr.Date) }
            //            equals new { ts.ProjectId, ts.UserId, Date = ts.StartDate }
            //        select new { ts.UserId, ts.Status, tr.Hours, tr.TaskId, tr.Date };

            string str = q.ToQueryString(); //.ToString();
            var list = await q.ToListAsync();
            var d = new Dictionary<TimeDictionary, double>();
            foreach ( var t in list )
            {
                var key = new TimeDictionary() { Date = DateOnly.FromDateTime(t.Date), Type = t.TaskId };
                d[key] = t.Hours;
            }
            return d;
        }
        public async Task<Dictionary<TimeDictionary, string>> GetStatus(int Id, DateTime start, DateTime end)
        {
            var dic = new Dictionary<TimeDictionary, string>();
            //var q = from ts in _context.TaskStatuses
            //        where ts.UserId == Id && ts.ProjectId == TimeOffId && ts.StartDate <= end && ts.StartDate >= start
            //        join vt in _context.VirtualTasks
            //            on ts.ProjectId
            //            equals vt.ProjectId
            //        select new { ts.UserId, ts.Status, vt.Id, tr.TaskId, tr.Date };
            var q = _context.TaskStatuses.Where(ts => ts.UserId == Id && ts.ProjectId == TimeOffId && ts.StartDate <= end && ts.StartDate >= start);
            string str = q.ToQueryString(); //.ToString();
            var list = await q.ToListAsync();
            var stlist = _context.VirtualTasks.Where(x => x.ProjectId == TimeOffId).Select(vt => vt.Id).ToList();
            for (var d = start; d <= end; )
            {
                foreach (var t in stlist)
                {
                    var key = new TimeDictionary() { Date = DateOnly.FromDateTime(d), Type = t };
                    dic[key] = list.Where(l => l.TaskId == t && l.StartDate == GetMonday(d)).Select(x => x.Status).FirstOrDefault() ?? Settings.StateNew;
                }
                d = d.AddDays(1);
            }
            return dic;
        }
        public async Task GetChronoUserIds(IEnumerable<IUserIdChrono> idChronos)
        {
            idChronos.ToList().ForEach(x => x.UserIdChrono = -1);
            var names = idChronos.Select(x => x.UserName).ToList();
            var list = await _context.Users.Where(u => u.DisplayName != null && names.Contains( u.DisplayName)).ToListAsync();
            foreach (var u in list) 
            {
                var user = idChronos.Where(x => x.UserName.Equals(u.DisplayName)).FirstOrDefault();
                if (user != null)
                {
                    user.UserIdChrono = u.UserId;
                }
            }
            return;
        }
        public async Task<int> SaveDaysOff(TimeOffModel timeOffModel)
        {
            int count = 0;
            foreach(var t in timeOffModel.Time)
            {
                var report = await _context.TimeReports
                    .FirstOrDefaultAsync(tr => tr.UserId == timeOffModel.UserId && tr.TaskId == t.Key.Type && tr.Date == t.Key.Date.ToDateTime(TimeOnly.Parse("0:0:0")));
                if(report == null)
                {
                    await AddReport(timeOffModel.UserId, t.Key.Type, t.Key.Date.ToDateTime(TimeOnly.Parse("0:0:0")), t.Value);
                    count++;
                }
            }
            return count; // await Task.FromResult(0);
        }
        private async Task AddReport(int userId, int taskId, DateTime date, double hours)
        {
            //var tr = await _context.TimeReports.FirstOrDefaultAsync(t => t.Type == 0);
            //int ty = tr.Type;
            //var tn = await _context.FieldInfos.FirstAsync(t => t.Id == 0);
            try
            {
                await StartTransaction();
                TimeReport report = new()
                {
                    Date = date,
                    UserId = userId,
                    TaskId = taskId,
                    ProjectId = TimeOffId,
                    LastUpdated = DateTime.UtcNow,
                    //Type = 0,
                    Hours = hours,
                    TypeNavigation = fieldInfo,

                };
                _context.TimeReports.Add(report);
                await _context.SaveChangesAsync();
                var q = _context.TaskStatuses
                    .FirstOrDefaultAsync(ts => ts.UserId == userId && ts.ProjectId == TimeOffId && ts.StartDate == GetMonday(date) && ts.TaskId == taskId);
                //string str = q.ToQueryString(); //.ToString();
                var status = await q;
                if(status == null)
                {
                    _context.TaskStatuses.Add(status = new()
                    { ProjectId = TimeOffId, StartDate = GetMonday(date), Status = Settings.StateAdd, TaskId = taskId, UserId = userId });
                }
                
                await _context.SaveChangesAsync();
                await CommitTransaction();
            }
            catch(Exception ex) 
            {
                await RollbackTransaction();
                //  Отловить и передать. Или же ловить выше...
            }
        }
        public async Task<string> Test()
        {
            var str = (await _context.VirtualProjects.FirstOrDefaultAsync()).Name;
            return str;
        }
        public static DateTime GetMonday(DateTime date)
        {
            //var ret = DateOnly.FromDateTime( date.AddDays(DayOfWeek.Monday - date.DayOfWeek ));
            var ret = date.AddDays(DayOfWeek.Monday - date.DayOfWeek);
            return ret;
        }
        private async Task StartTransaction()
        {
            await _context.Database.BeginTransactionAsync();
        }
        private async Task CommitTransaction()
        {
            await _context.Database.CommitTransactionAsync();
        }
        private async Task RollbackTransaction()
        {
            await _context.Database.RollbackTransactionAsync();
        }
    }
}
