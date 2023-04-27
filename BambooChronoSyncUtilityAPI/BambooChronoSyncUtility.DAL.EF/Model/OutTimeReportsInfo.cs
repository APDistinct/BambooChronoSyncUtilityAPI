using System;
using System.Collections.Generic;

namespace BambooChronoSyncUtility.DAL.EF.Model
{
    public partial class OutTimeReportsInfo
    {
        public OutTimeReportsInfo()
        {
            OutTimeReportsData = new HashSet<OutTimeReportsDatum>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime LoadDate { get; set; }
        public string FileName { get; set; } = null!;
        public int OutProjectInfoPatternId { get; set; }

        public virtual OutProjectInfoPattern OutProjectInfoPattern { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual ICollection<OutTimeReportsDatum> OutTimeReportsData { get; set; }
    }
}
