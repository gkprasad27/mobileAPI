using System;
using System.Collections.Generic;

namespace mobileAPI.Models
{
    public partial class AppUser
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string CompanyCode { get; set; }
        public string EmpCode { get; set; }
        public string ProfileImage { get; set; }
    }
}
