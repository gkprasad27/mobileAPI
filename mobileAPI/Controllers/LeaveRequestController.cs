using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using mobileAPI.BussinessLogic;
using mobileAPI.Models;

namespace mobileAPI.Controllers
{
    [Route("api/LeaveRequest")]
    [ApiController]
    public class LeaveRequestController : ControllerBase
    {

        public const string IN = "IN";
        public const string OUT = "OUT";

        [HttpGet("IsEmployeePuchIn/{employeeCode}")]
        public async Task<IActionResult> IsEmployeePuchIn(string employeeCode)
        {
            if (string.IsNullOrEmpty(employeeCode))
                return Ok(new APIResponse() { STATUS = APISTATUS.FAIL.ToString(), Response = "employee code missing in query string." });

            try
            {
                var result = new leaveRequestHelper().GetAttendanceList(employeeCode, null, null);
                if (result.Count > 0)
                {
                    string direction = result.OrderByDescending(x => x.Date).First().Direction;
                    if (direction.Equals(IN))
                    {
                        return Ok(new APIResponse() { STATUS = APISTATUS.PASS.ToString(), Response = true });
                    }
                    else if (direction.Equals(OUT))
                    {
                        return Ok(new APIResponse() { STATUS = APISTATUS.FAIL.ToString(), Response = "Already punch out." });
                    }
                }

                return Ok(new APIResponse() { STATUS = APISTATUS.PASS.ToString(), Response = false });
            }
            catch (Exception ex)
            {
                return Ok(new APIResponse() { STATUS = APISTATUS.FAIL.ToString(), Response = ex.Message });
            }
        }

        //[HttpGet("GetAttendance/{employeeCode}/{fromDate?}/{toDate?}")]
        //public async Task<IActionResult> IsEmployeePuchIn(string employeeCode, DateTime? fromDate, DateTime? toDate)
        //{
        //    if (string.IsNullOrEmpty(employeeCode))
        //        return Ok(new APIResponse() { STATUS = APISTATUS.FAIL.ToString(), Response = "employee code missing in query string." });

        //    try
        //    {
        //        var result = new leaveRequestHelper().GetAttendanceList(employeeCode, fromDate, toDate);
        //        if (result.Count > 0)
        //        {
        //            return Ok(new APIResponse() { STATUS = APISTATUS.PASS.ToString(), Response = result });
        //        }

        //        return Ok(new APIResponse() { STATUS = APISTATUS.FAIL.ToString(), Response = "No Data Found." });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(new APIResponse() { STATUS = APISTATUS.FAIL.ToString(), Response = ex.Message });
        //    }
        //}

        [HttpPost("ApplyLeaveRequest")]
        public async Task<IActionResult> ApplyLeaveRequest([FromBody]LeaveApplDetails leaveApplDetails)
        {
            if (leaveApplDetails == null)
            {
                return Ok(new APIResponse() { STATUS = APISTATUS.FAIL.ToString(), Response = "Request is empty." });
            }

            try
            {
                RETIRNSTATUS status;
                string errorMsg = string.Empty;

                LeaveApplDetails result = new leaveRequestHelper().InsertLeaveApplDetails(leaveApplDetails, out status, out errorMsg);
                if (status == RETIRNSTATUS.PASS)
                {
                    return Ok(new APIResponse() { STATUS = APISTATUS.PASS.ToString(), Response = result });
                }

                return Ok(new APIResponse() { STATUS = APISTATUS.FAIL.ToString(), Response = errorMsg });
            }
            catch (Exception ex)
            {
                return Ok(new APIResponse() { STATUS = APISTATUS.FAIL.ToString(), Response = ex.Message });
            }
        }

        [HttpGet("GetApplyLeaves/{employeeCode}")]
        public async Task<IActionResult> GetApplyLeaves(string employeeCode)
        {
            if (string.IsNullOrEmpty(employeeCode))
                return Ok(new APIResponse() { STATUS = APISTATUS.FAIL.ToString(), Response = "employee code missing in query string." });

            try
            {
                List<LeaveApplDetails> result = new leaveRequestHelper().GetLeaveApplDetailsList(employeeCode);
                if (result.Count > 0)
                {
                    return Ok(new APIResponse() { STATUS = APISTATUS.PASS.ToString(), Response = result });
                }

                return Ok(new APIResponse() { STATUS = APISTATUS.FAIL.ToString(), Response = "No Data Found." });
            }
            catch (Exception ex)
            {
                return Ok(new APIResponse() { STATUS = APISTATUS.FAIL.ToString(), Response = ex.Message });
            }
        }

        //[HttpGet("GetLeaveApplDetailsForRecomand/{employeeCode}")]
        //public async Task<IActionResult> GetLeaveApplDetailsList(string employeeCode)
        //{
        //    if (string.IsNullOrEmpty(employeeCode))
        //        return Ok(new APIResponse() { STATUS = APISTATUS.FAIL.ToString(), Response = "employee code missing in query string." });

        //    try
        //    {
        //        List<LeaveApplDetails> leaveTypes = new leaveRequestHelper().GetLeaveApplDetailsForRecomand(employeeCode);
        //        if (leaveTypes.Count > 0)
        //        {
        //            return Ok(new APIResponse() { STATUS = APISTATUS.PASS.ToString(), Response = leaveTypes });
        //        }

        //        return Ok(new APIResponse() { STATUS = APISTATUS.FAIL.ToString(), Response = "No Data Found." });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(new APIResponse() { STATUS = APISTATUS.FAIL.ToString(), Response = ex.Message });
        //    }
        //}

        [HttpGet("GetLeaveApplDetailsForApproval/{employeeCode}")]
        public async Task<IActionResult> GetLeaveApplDetailsForApproval(string employeeCode)
        {
            if (string.IsNullOrEmpty(employeeCode))
                return Ok(new APIResponse() { STATUS = APISTATUS.FAIL.ToString(), Response = "employee code missing in query string." });

            try
            {
                List<LeaveApplDetails> result = new leaveRequestHelper().GetLeaveApplDetailsForApproval(employeeCode);
                if (result.Count > 0)
                {
                    return Ok(new APIResponse() { STATUS = APISTATUS.PASS.ToString(), Response = result });
                }

                return Ok(new APIResponse() { STATUS = APISTATUS.FAIL.ToString(), Response = "No Data Found." });
            }
            catch (Exception ex)
            {
                return Ok(new APIResponse() { STATUS = APISTATUS.FAIL.ToString(), Response = ex.Message });
            }
        }

        [HttpPost("LeaveApprovalProcess/{employeeCode}")]
        public async Task<IActionResult> LeaveApprovalProcess(string employeeCode, [FromBody]List<LeaveApplDetails> leaveApplDetails)
        {
            if (string.IsNullOrEmpty(employeeCode) || leaveApplDetails.Count == 0)
                return Ok(new APIResponse() { STATUS = APISTATUS.FAIL.ToString(), Response = "Request is empty." });

            try
            {
                List<LeaveApplDetails> result = new leaveRequestHelper().LeaveApprovalProcess(employeeCode, leaveApplDetails);
                if (result.Count > 0)
                {
                    return Ok(new APIResponse() { STATUS = APISTATUS.PASS.ToString(), Response = result });
                }

                return Ok(new APIResponse() { STATUS = APISTATUS.FAIL.ToString(), Response = "Failed to process." });
            }
            catch (Exception ex)
            {
                return Ok(new APIResponse() { STATUS = APISTATUS.FAIL.ToString(), Response = ex.Message });
            }
        }

        [HttpGet("GetLeaveStatuses")]
        public async Task<IActionResult> GetLeaveStatuses()
        {
            try
            {
                List<string> result = new leaveRequestHelper().GetLeavStatusList();

                if (result.Count > 0)
                {
                    dynamic expando = new ExpandoObject();
                    expando.LeavesStatuses = result.Select(x => new { ID = x, TEXT = x });
                    return Ok(new APIResponse() { STATUS = APISTATUS.PASS.ToString(), Response = expando });
                }

                return Ok(new APIResponse() { STATUS = APISTATUS.FAIL.ToString(), Response = "No leave status Found." });
            }
            catch (Exception ex)
            {
                return Ok(new APIResponse() { STATUS = APISTATUS.FAIL.ToString(), Response = ex.Message });
            }
        }

    }
}