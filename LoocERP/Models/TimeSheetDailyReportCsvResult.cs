using LoocERP.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LoocERP.Models
{
    public class TimeSheetDailyReportCsvResult : FileResult
    {
        private readonly IEnumerable<DailyReportElementViewModel> _employeeData;

        public TimeSheetDailyReportCsvResult(IEnumerable<DailyReportElementViewModel> employeeData, string fileDownloadName) : base("text/csv")
        {
            _employeeData = employeeData;
            FileDownloadName = fileDownloadName;
        }
        public async override Task ExecuteResultAsync(ActionContext context)
        {
            var response = context.HttpContext.Response;
            context.HttpContext.Response.Headers.Add("Content-Disposition", new[] { "attachment; filename=" + FileDownloadName });
            using (var streamWriter = new StreamWriter(response.Body))
            {
                await streamWriter.WriteLineAsync(
                  $"Name"
                );
                foreach (var p in _employeeData)
                {
                    await streamWriter.WriteLineAsync(
                      $"{p.User.FirstName} {p.User.LastName}"
                    );
                    await streamWriter.FlushAsync();
                }
                await streamWriter.FlushAsync();
            }
        }
    }
}

