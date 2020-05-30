using mobileAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mobileAPI.BussinessLogic
{
    public class BioMatricAttendanceHleper
    {
        public BiometricAttendance InsertPuchInOutRecord(BiometricAttendance biometricAttendance)
        {
            try
            {
                using (MobileContext context = new MobileContext())
                {
                    biometricAttendance.Status = "P";
                    biometricAttendance.Date = DateTime.Now;

                    context.BiometricAttendance.Add(biometricAttendance);
                    if (context.SaveChanges() > 0)
                        return biometricAttendance;


                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BiometricAttendance> GetAttendances(string empCode,DateTime? fromDate, DateTime? toDate)
        {
            try
            {
                List<BiometricAttendance> attendanceList = null;

                if(fromDate == null)
                    fromDate = DateTime.Now;

                if (toDate == null)
                    toDate = DateTime.Now;

                using (MobileContext context=new MobileContext())
                {
                  
                          attendanceList= context.BiometricAttendance.AsEnumerable()
                                                  .Where(b=> b.EmpCode == empCode
                                                          && Convert.ToDateTime(b.Date.Value.ToShortDateString()) <= Convert.ToDateTime(toDate.Value.ToShortDateString())
                                                          && Convert.ToDateTime(b.Date.Value.ToShortDateString()) <= Convert.ToDateTime(toDate.Value.ToShortDateString()))
                                                  .OrderByDescending(x=> x.Date)
                                                  .ToList();


                    return attendanceList;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
