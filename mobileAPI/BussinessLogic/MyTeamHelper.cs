using mobileAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mobileAPI.BussinessLogic
{
    public class MyTeamHelper
    {

        public Dictionary<Employees, BiometricAttendance> GetMyTeam(string employeeId)
        {
            try
            {
                Dictionary<Employees, BiometricAttendance> employeeAttenadance = new Dictionary<Employees, BiometricAttendance>();

                using (MobileContext context=new MobileContext())
                {
                    var employees= context.Employees
                                  .Where(emp=> emp.RecommendedBy == employeeId
                                           || emp.ApprovedBy == employeeId)
                                  .ToList();

                    foreach(var emp in employees)
                    {
                        var _attendance = context.BiometricAttendance.Where(x => x.EmpCode == emp.Code).OrderByDescending(x => x.Date).FirstOrDefault();
                        employeeAttenadance.Add(emp, _attendance);
                    }
                }

                return employeeAttenadance;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


    }
}
