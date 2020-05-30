using System;
using System.Collections.Generic;

namespace mobileAPI.Models
{
    public partial class Product
    {
        public int? Id { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string Remarks { get; set; }
        public string Ext1 { get; set; }
        public string Active { get; set; }
    }
}
