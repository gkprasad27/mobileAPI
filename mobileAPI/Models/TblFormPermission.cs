using System;
using System.Collections.Generic;

namespace mobileAPI.Models
{
    public partial class TblFormPermission
    {
        public int? Id { get; set; }
        public string FormId { get; set; }
        public int? RoleId { get; set; }
        public string Remarks { get; set; }
    }
}
