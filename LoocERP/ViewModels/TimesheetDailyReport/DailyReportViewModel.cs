using LoocERP.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LoocERP.ViewModels
{
    public class DailyReportViewModel
    {
        public DailyReportViewModel()
        {            
            TimeSheetDailyReportList = new List<DailyReportElementViewModel>();
        }        
        public List<DailyReportElementViewModel> TimeSheetDailyReportList { get; set; }
        public AppUser User { get; set; }
        public Turno Turno { get; set; }
        public Cantiere Cantiere { get; set; }
        public Project Project { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public ContractUser Contract { get; set;}


    }
}