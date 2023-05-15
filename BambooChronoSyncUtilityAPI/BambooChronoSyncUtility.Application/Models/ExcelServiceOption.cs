using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BambooChronoSyncUtility.Application.Models
{
    public class ExcelServiceOption
    {
        public string FileName { get; set; } = null!;
        public string IDColumnName { get; set; } = null!;
        public string EmployeeColumnName { get; set; } = null!;
    }
}
