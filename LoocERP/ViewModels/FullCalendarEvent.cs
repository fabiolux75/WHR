using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LoocERP.ViewModels
{
    public class FullCalendarEvent
    {                
        public int id { get; set; }
        public string title { get; set; }
        public string allDay { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public TimeSpan HourDiff { get; set; }
    }
}
