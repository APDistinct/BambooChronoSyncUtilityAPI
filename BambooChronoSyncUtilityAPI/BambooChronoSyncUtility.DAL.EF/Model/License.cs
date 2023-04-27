using System;
using System.Collections.Generic;

namespace BambooChronoSyncUtility.DAL.EF.Model
{
    public partial class License
    {
        public int Id { get; set; }
        public Guid LicenseKey { get; set; }
    }
}
