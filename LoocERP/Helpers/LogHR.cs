using System;
using System.Linq;
using LoocERP.Models;
using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using static LoocERP.Models.LogAuditHR;

namespace LoocERP.Helpers
{
    
    public class LogHR
    {
        private static LogHR _instance=null;
        private static float hoursTimeout = 5;

        public static LogHR Instance
        {
            get
            {
                if(_instance==null) _instance=new LogHR();
                return _instance;
            }
        }

        public void Log(Data.ApplicationDBContext _context, AppUser user, LogAuditHREventType? eventType = 0, String? Details = "" , bool alwaysLog = true )  
        {
            if (!alwaysLog)
            {
                var num = _context.Set<LogAuditHR>().Where(c => c.UserId == user.Id && c.EventType == eventType && EF.Functions.DateDiffHour(c.EventDate, DateTime.Now) < hoursTimeout).Count();
                if(num > 0)
                {
                    return;
                }
            }

            try{
                LogAuditHR c = new LogAuditHR();
                c.Id = Guid.NewGuid();
                c.UserId = user.Id;
                c.EventType = eventType;
                c.EventDate = DateTime.Now;
                c.Details = Details;
                c.MultiTenantId = user.MultiTenantId;

                _context.Set<LogAuditHR>().Add(c);

                _context.SaveChanges();

            }catch(Exception e){
                Console.Write(e.Message);
            }
        }  
    }
}