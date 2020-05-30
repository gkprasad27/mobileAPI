using System;
using System.Collections.Generic;

namespace mobileAPI.Models
{
    public partial class LeaveRequest
    {
        public int? Id { get; set; }
        public string EmpCode { get; set; }
        public string EmpName { get; set; }
        public DateTime? ApplyDate { get; set; }
        public string LeaveType { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string NoOfDays { get; set; }
        public string Reason { get; set; }
        public string Upload { get; set; }
        public string Description { get; set; }
        public string FromMornig { get; set; }
        public string FromEvening { get; set; }
        public string ToMorning { get; set; }
        public string ToEvening { get; set; }
        public string Subject { get; set; }
    }
}
