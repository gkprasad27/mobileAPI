using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mobileAPI.BussinessLogic
{
    public static class CommanHelper
    {

        public static string FormatDatess(this DateTime value, string format = "yyyy-MM-dd")
        {
            return ((DateTime)value).ToString(format);
        }


        public static bool IsDateBetweenDates(this DateTime? value, DateTime? startDate,DateTime? endDate)
        {
            value = Convert.ToDateTime(((DateTime)value).ToString("yyyy-MM-dd"));
            startDate = Convert.ToDateTime(((DateTime)startDate).ToString("yyyy-MM-dd"));
            endDate = Convert.ToDateTime(((DateTime)endDate).ToString("yyyy-MM-dd"));

            return (value >= startDate && value <= endDate);
        }
    }
}
