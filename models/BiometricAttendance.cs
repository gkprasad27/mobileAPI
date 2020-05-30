using System;
using System.Collections.Generic;

namespace mobileAPI.Models
{
    public partial class BiometricAttendance
    {
        public int Id { get; set; }
        public string EmpCode { get; set; }
        public string Location { get; set; }
        public string InTime { get; set; }
        public string OutTime { get; set; }
        public DateTime? Date { get; set; }
        public string Status { get; set; }
        public string Ext { get; set; }
        public string Remarks { get; set; }
        public string CompanyCode { get; set; }
        public string Direction { get; set; }
        public string Device { get; set; }
    }
}
