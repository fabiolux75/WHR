using LoocERP.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LoocERP.ViewModels
{
    public class DailyReportElementViewModel
    {
        public DailyReportElementViewModel()
        {            
            Turno = new Turno();                        
            User = new AppUser();
            Commessa = new Project();                        
            Cantiere = new Cantiere();
            TimeSheetDailyReport = new TimeSheetDailyReport();
            WorkDate = DateTime.Now;            
        }

        public DateTime WorkDate;        
        public Project Commessa;
        public Cantiere Cantiere;
        public Turno Turno;
        public AppUser User;

        public TimeSheetDailyReport TimeSheetDailyReport;
    }
}