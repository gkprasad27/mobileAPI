using System;
using System.Collections.Generic;

namespace mobileAPI.Models
{
    public partial class VisitResion
    {
        public int Id { get; set; }
        public string VisitType { get; set; }
        public string VisitName { get; set; }
        public string Remarks { get; set; }
        public string Ext1 { get; set; }
    }
}
