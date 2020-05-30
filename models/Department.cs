﻿using System;
using System.Collections.Generic;

namespace mobileAPI.Models
{
    public partial class Department
    {
        public string DepartmentId { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyDesc { get; set; }
        public string CompanyGroupCode { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string DepartmentName { get; set; }
        public bool IsActive { get; set; }
        public string ResponsiblePersonCode { get; set; }
        public string ResponsiblePersonDesc { get; set; }
        public string Active { get; set; }
        public DateTime? AddDate { get; set; }
    }
}
