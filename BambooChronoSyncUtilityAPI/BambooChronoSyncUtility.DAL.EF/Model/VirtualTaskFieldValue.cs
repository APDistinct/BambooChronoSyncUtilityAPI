using System;
using System.Collections.Generic;

namespace BambooChronoSyncUtility.DAL.EF.Model
{
    public partial class VirtualTaskFieldValue
    {
        public int TaskId { get; set; }
        public int FieldId { get; set; }
        public string? Value { get; set; }

        public virtual FieldInfo Field { get; set; } = null!;
        public virtual VirtualTask Task { get; set; } = null!;
    }
}
