using OfficeOpenXml;

namespace BambooChronoSyncUtility.Service.Repositories
{
    public static class ExcelExtensionMethods
    {
        public static int GetColumnByName(this ExcelWorksheet ws, string columnName)
        {
            if (ws == null) throw new ArgumentNullException(nameof(ws));
            return ws.Cells["1:1"].First(c => c.Value.ToString() == columnName).Start.Column;
        }
    }
}
