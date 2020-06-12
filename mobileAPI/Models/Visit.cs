using System;
using System.Collections.Generic;

namespace mobileAPI.Models
{
    public partial class Visit
    {
        public int VisitId { get; set; }
        public string ClientName { get; set; }
        public string Locatation { get; set; }
        public string ContactPerson { get; set; }
        public string EmailId { get; set; }
        public string Purpose { get; set; }
        public string Product { get; set; }
        public string Remark { get; set; }
        public string VisitedEmployee { get; set; }
        public DateTime? AddDate { get; set; }
        public string VisitType { get; set; }
        public string PotentialLead { get; set; }
        public string ContactNumber { get; set; }
    }
}
