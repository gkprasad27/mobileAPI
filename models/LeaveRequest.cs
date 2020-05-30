using System;
using System.Collections.Generic;

namespace mobileAPI.Models
{
    public partial class LeaveRequest
    {
        public int Id { get; set; }
        public string EmpCode { get; set; }
        public string EmpName { get; set; }
        public string ApplyDate { get; set; }
        public string LeaveType { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string NoOfDays { get; set; }
        public string Reason { get; set; }
        public string Upload { get; set; }
        public string Description { get; set; }
    }
}
