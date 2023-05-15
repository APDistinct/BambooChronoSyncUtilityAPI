using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BambooChronoSyncUtility.Application
{
    public static class Settings
    {
        public static List<string> StatesWork = new List<string> { "Added", "Declined" };
        public static string StateNew = "New";
        public static List<string> StatesAll = new List<string>() { "Added", "Declined", "New" };
    }
}
