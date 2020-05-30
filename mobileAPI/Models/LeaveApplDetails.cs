using System;
using System.Collections.Generic;

namespace mobileAPI.Models
{
    public partial class LeaveApplDetails
    {
        public int? Sno { get; set; }
        public string EmpCode { get; set; }
        public string EmpName { get; set; }
        public string ApplDate { get; set; }
        public string LeaveCode { get; set; }
        public DateTime? LeaveFrom { get; set; }
        public DateTime? LeaveTo { get; set; }
        public double? LeaveDays { get; set; }
        public string HalfdayFrom { get; set; }
        public string HalfDayTo { get; set; }
        public string LeaveRemarks { get; set; }
        public string Status { get; set; }
        public string ApprovedId { get; set; }
        public string ApprovedName { get; set; }
        public string Reason { get; set; }
        public string RecommendedId { get; set; }
        public string RecommendedName { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string AcceptedRemarks { get; set; }
        public string CompanyCode { get; set; }
        public string Ext1 { get; set; }
        public byte[] Ext2 { get; set; }
        public DateTime? AddDate { get; set; }
        public string FromMorning { get; set; }
        public string FromEvening { get; set; }
        public string ToMorning { get; set; }
        public string ToEvening { get; set; }
        public string Subject { get; set; }
    }
}
