using System;
using System.Collections.Generic;

namespace mobileAPI.Models
{
    public partial class TblTourBills
    {
        public int? Id { get; set; }
        public string Tourid { get; set; }
        public string EmpId { get; set; }
        public string EmpName { get; set; }
        public string UploadIamge { get; set; }
        public double? BillAmount { get; set; }
        public string RecommendedPersion { get; set; }
        public string ApprovedPersion { get; set; }
        public DateTime? RecommendedDate { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public DateTime? AppliedDate { get; set; }
        public string Description { get; set; }
        public string Narration { get; set; }
        public string ImageName { get; set; }
        public string ImageContent { get; set; }
        public string ImageExtension { get; set; }
    }
}
