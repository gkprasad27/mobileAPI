using System;
using System.Dynamic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mobileAPI.BussinessLogic;
using mobileAPI.Models;

namespace mobileAPI.Controllers
{
    [Route("api/BioMatricAttendance/")]
    [ApiController]
    public class BioMatricAttendanceController : ControllerBase
    {
        [HttpPost("BioMatricAttendance")]
        public async Task<IActionResult> BioMatricAttendance([FromBody]BiometricAttendance biometricAttendance)
        {
            if (biometricAttendance == null)
            {
                return Ok(new APIResponse() { STATUS = APISTATUS.FAIL.ToString(), Response = "Request is empty." });
            }

            try
            {
                BiometricAttendance result = new BioMatricAttendanceHleper().InsertPuchInOutRecord(biometricAttendance);
                if (result != null)
                {
                    return Ok(new APIResponse() { STATUS = APISTATUS.PASS.ToString(), Response = result });
                }

                return Ok(new APIResponse() { STATUS = APISTATUS.FAIL.ToString(), Response = "Attendance Punching failed failed" });
            }
            catch (Exception ex)
            {
                return Ok(new APIResponse() { STATUS = APISTATUS.FAIL.ToString(), Response = ex.Message });
            }
        }

        [HttpPost("GetAttendances")]
        public async Task<IActionResult> GetAttendances([FromBody]SearchCriteria searchCriteria)
        {
            try
            {
                var attendanceList = new BioMatricAttendanceHleper().GetAttendances(searchCriteria.EmployeeCode, searchCriteria.FromDate, searchCriteria.ToDate);
                if (attendanceList.Count > 0)
                {
                    dynamic expando = new ExpandoObject();
                    expando.AttendanceList = attendanceList;
                    return Ok(new APIResponse() { STATUS = APISTATUS.PASS.ToString(), Response = expando });
                }

                return Ok(new APIResponse() { STATUS = APISTATUS.FAIL.ToString(), Response = "No Data found." });
            }
            catch(Exception ex)
            {
                return Ok(new APIResponse() { STATUS=APISTATUS.FAIL.ToString(),Response= ex .Message});
            }
        }
    }
}
