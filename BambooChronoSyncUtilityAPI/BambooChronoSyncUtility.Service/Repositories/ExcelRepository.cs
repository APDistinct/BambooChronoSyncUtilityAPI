using Microsoft.Extensions.Configuration;
using BambooChronoSyncUtility.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using OfficeOpenXml;

namespace BambooChronoSyncUtility.Service.Repositories
{
    public interface IExcelRepository
    {
        Task<IEnumerable<UserIdBambooHR>> GetUserIds();
    }
    public class ExcelRepository : IExcelRepository
    {
        //private readonly IConfiguration _config;
        private readonly ExcelServiceOption _options;
        public ExcelRepository(IOptions<ExcelServiceOption> options )
        {
            //_config = config;
            _options = options.Value;
        }
        public async Task<IEnumerable<UserIdBambooHR>> GetUserIds()
        {
            List<UserIdBambooHR > userIds = new List<UserIdBambooHR>();
            string s1 = _options.FileName;
            string s2 = _options.IDColumnName;
            string s3 = _options.EmployeeColumnName;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var excelPackage = new ExcelPackage(new FileInfo(s1)))
            {
                var worksheet = excelPackage.Workbook.Worksheets.First();
                var startRow = worksheet.Dimension.Start.Row + 1;
                var endRow = worksheet.Dimension.End.Row;

                var columnId = worksheet.GetColumnByName(s2);
                var columnEmployee = worksheet.GetColumnByName(s3);
                for (var i = startRow; i <= endRow; i++)
                {
                    var itemId = worksheet.Cells[i, columnId].Value.ToString();
                    var employee = worksheet.Cells[i, columnEmployee].Value.ToString();
                    userIds.Add(new UserIdBambooHR() { UserIdBamboo = itemId, UserName = employee, });
                    //  Убрать проверку уровнем выше
                    //if (!IsInt(itemId) )
                    {
                        // Сообщение об ошибке

                        //validationStatus.Messages.Add(new MessageViewModel
                        //{
                        //    Status = "Error",
                        //    Message = $"Invalid user data for {employee}: ID(Excel) {itemId}, Sickness {itemSickness}, Vacation {itemVacation}"
                        //});
                    }
                }
            }

            return await Task.FromResult(userIds);
        }
       
    }
}
