using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mobileAPI
{
    public class APIResponse
    {
        public string STATUS { get; set; }
        public object Response { get; set; }
    }


    public enum APISTATUS
    {
        PASS,FAIL
    }


    public class SearchCriteria
    {
        private DateTime? _fromDate;
        private DateTime? _toDate;
        public DateTime? FromDate
        {
            get
            {
                return _fromDate;
            }
            set
            {
                if (value != null) ;

                _fromDate = value;
            }
        }
        public DateTime? ToDate
        {
            get
            {
                return _toDate;
            }
            set
            {
                if (value != null) ;

                    _toDate =value;
            }
        }
        public string EmployeeCode { get; set; }
    }
}
