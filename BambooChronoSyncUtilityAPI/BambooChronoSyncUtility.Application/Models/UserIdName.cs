using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BambooChronoSyncUtility.Application.Models
{
    public class UserIdName : IUserIdChrono, IUserIdBamboo
    {
        public int UserIdChrono { get; set; }
        public string UserName { get; set; } = string.Empty;
        public int UserIdBamboo { get; set; }
    }
}
