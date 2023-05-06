using BambooChronoSyncUtility.DAL.EF.Model;
using BambooChronoSyncUtility.Service.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BambooChronoSyncUtility.Service.Repositories
{
    public interface IChronoRepository
    {
        Task<Dictionary<TimeDictionary, double>> GetTimeOff(int Id, DateTime start, DateTime end);
        Task<Dictionary<TimeDictionary, string>> GetStatus(int Id, DateTime start, DateTime end);
    }
    public class ChronoRepository : IChronoRepository
    {
        private readonly int TimeOffId = -1; //900;
        private readonly ChronoContext _context;
        
        public ChronoRepository(ChronoContext context) => _context = context;
        
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
                    dic[key] = list.Where(l => l.TaskId == t && l.StartDate == GetMonday(d)).Select(x => x.Status).FirstOrDefault() ?? "New";
                }
                d = d.AddDays(1);
            }
            return dic;
        }
        public static DateTime GetMonday(DateTime date)
        {
            //var ret = DateOnly.FromDateTime( date.AddDays(DayOfWeek.Monday - date.DayOfWeek ));
            var ret = date.AddDays(DayOfWeek.Monday - date.DayOfWeek);
            return ret;
        }
    }
}
