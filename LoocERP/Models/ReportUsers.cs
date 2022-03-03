using System;
using System.Collections.Generic;
using System.Linq;

namespace LoocERP.Models
{

    public class ReportUsers
    {

        public string Id { get; set; }
        //public string MansioniDue { get; set; }
        public IEnumerable<Mansione> Mansioni { get; set; }
        //public string Mansioni { get; set; }
        public string FirstName { get; set; } 
        public string LastName  { get; set; }
        public string PhoneNumber  { get; set; }
        public string InternalCode  { get; set; }
        public DateTime? DataModifica  { get; set; }
        public DateTime? DataNascita  { get; set; }
        public string Citta  { get; set; }
        public ContractUser contract { get; set; }


        
        



    }
}
