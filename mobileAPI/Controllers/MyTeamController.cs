using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using mobileAPI.BussinessLogic;

namespace mobileAPI.Controllers
{
    [Route("api/MyTeam")]
    [ApiController]
    public class MyTeamController : ControllerBase
    {
        [HttpGet("GetTeamMembers/{employeeID}")]
        public async Task<IActionResult> GetMenus(string employeeID)
        {
            try
            {
                if (string.IsNullOrEmpty(employeeID))
                {
                    return Ok(new APIResponse() { STATUS = APISTATUS.FAIL.ToString(), Response = "Query string parameter missing." });
                }

                var response = new MyTeamHelper().GetMyTeam(employeeID);
                dynamic expando = new ExpandoObject();
                expando.TeamList = response.Select(emp => new { EmployeeCode = emp.Key.Code, EmployeeName = emp.Key.Name ,LastAttendance = emp.Value});

                return Ok(new APIResponse() { STATUS = APISTATUS.PASS.ToString(), Response = expando });
            }
            catch (Exception ex)
            {
                return Ok(new APIResponse() { STATUS = APISTATUS.FAIL.ToString(), Response = ex.Message });
            }
        }
    }
}