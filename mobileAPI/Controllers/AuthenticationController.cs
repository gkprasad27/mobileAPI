using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using mobileAPI.BussinessLogic;
using mobileAPI.Models;

namespace mobileAPI.Controllers
{
    [ApiController]
    [Route("api/Auth/")]
    public class AuthenticationController : ControllerBase
    {
      
        //public AuthenticationController(ILogger<AuthenticationController> logger)
        //{
        //    _logger = logger;
        //}

        [HttpPost("GetUserAuthenticity")]
        public async Task<IActionResult> login([FromBody]AppUser appUser)
        {
            try
            {
                if (string.IsNullOrEmpty(appUser.UserName))
                {
                    return Ok(new APIResponse() { STATUS = APISTATUS.FAIL.ToString(), Response = "User name can not be empty." });
                }

                if (string.IsNullOrEmpty(appUser.Password))
                {
                    return Ok(new APIResponse() { STATUS = APISTATUS.FAIL.ToString(), Response = "Password can not be empty." });
                }

                var response = new AuthentocationHelper().ValidateUser(appUser);
                if (response == null)
                {
                    return Ok(new APIResponse() { STATUS = APISTATUS.FAIL.ToString(), Response = "User not found." });
                }


                return Ok(new APIResponse() { STATUS = APISTATUS.PASS.ToString(), Response = response });
            }
            catch (Exception ex)
            {
                return Ok(new APIResponse() { STATUS = APISTATUS.FAIL.ToString(), Response = ex.Message });
            }
        }


        [HttpGet("GetMenus/{roleId}")]
        public async Task<IActionResult> GetMenus(string roleId)
        {
            try
            {
                if (string.IsNullOrEmpty(roleId))
                {
                    return Ok(new APIResponse() { STATUS = APISTATUS.FAIL.ToString(), Response = "Query string parameter missing." });
                }

                string errorMessage = string.Empty;


                var response = new AuthentocationHelper().GetScreenNamesForRole(roleId,out errorMessage);
                if (response == null)
                {
                    return Ok(new APIResponse() { STATUS = APISTATUS.FAIL.ToString(), Response = errorMessage });
                }


                return Ok(new APIResponse() { STATUS = APISTATUS.PASS.ToString(), Response = response });
            }
            catch (Exception ex)
            {
                return Ok(new APIResponse() { STATUS = APISTATUS.FAIL.ToString(), Response = ex.Message });
            }
        }
    }
}
