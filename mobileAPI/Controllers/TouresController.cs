using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using mobileAPI.BussinessLogic;
using mobileAPI.Config;
using mobileAPI.Models;

namespace mobileAPI.Controllers
{
    
    [ApiController]
    [Route("api/Toures/")]
    public class TouresController : ControllerBase
    {
        IWebHostEnvironment _webHostEnvironment = null;
        private readonly IOptions<ApplicationConfig> applicationConfig;
        IConfiguration iConfig = null;


        public TouresController(IWebHostEnvironment environment, IOptions<ApplicationConfig> appConfig, IConfiguration iConfig)
        {
            _webHostEnvironment = environment;
            applicationConfig = appConfig;
            this.iConfig = iConfig;
        }
        [HttpGet("GetVisitTypes")]
        public async Task<IActionResult> GetVisitTypes()
        {
            try
            {
                List<string> vistTypes = new TouresHelper().GetVisitTypes();
                if (vistTypes.Count > 0)
                {
                    dynamic expando = new ExpandoObject();
                    expando.VisitTypes = vistTypes.Select(x => new { ID = Regex.Replace(x,@"\s+",string.Empty).ToUpper(), TEXT = x });
                    return Ok(new APIResponse() { STATUS = APISTATUS.PASS.ToString(), Response = expando });
                }

                return Ok(new APIResponse() { STATUS = APISTATUS.FAIL.ToString(), Response = "Visit types not found." });
            }
            catch (Exception ex)
            {
                return Ok(new APIResponse() { STATUS = APISTATUS.FAIL.ToString(), Response = ex.Message });
            }
        }

        [HttpGet("GetVisitResions/{visittype}")]
        public async Task<IActionResult> ApplyLeaveRequest(string visittype)
        {
            if (string.IsNullOrEmpty(visittype))
                return Ok(new APIResponse() { STATUS = APISTATUS.FAIL.ToString(), Response = "In Request Visit types missing." });
            try
            {
                List<VisitResion> visitResions = new TouresHelper().GetVisitResions(visittype);
                if (visitResions.Count > 0)
                {
                    dynamic expando = new ExpandoObject();
                    expando.VisitResion = visitResions.Select(x => new { ID = x.Id, TEXT = x.VisitName });
                    return Ok(new APIResponse() { STATUS = APISTATUS.PASS.ToString(), Response = expando });
                }

                return Ok(new APIResponse() { STATUS = APISTATUS.FAIL.ToString(), Response = "Visit types not found." });
            }
            catch (Exception ex)
            {
                return Ok(new APIResponse() { STATUS = APISTATUS.FAIL.ToString(), Response = ex.Message });
            }
        }

        [HttpGet("GetProductList")]
        public async Task<IActionResult> GetProductList()
        {
            try
            {
                List<Product> products = new TouresHelper().GetProducts();
                if (products.Count > 0)
                {
                    dynamic expando = new ExpandoObject();
                    expando.Products = products.Select(x => new { ID = x.ProductId, TEXT = x.ProductName });
                    return Ok(new APIResponse() { STATUS = APISTATUS.PASS.ToString(), Response = expando });
                }

                return Ok(new APIResponse() { STATUS = APISTATUS.FAIL.ToString(), Response = "Visit types not found." });
            }
            catch (Exception ex)
            {
                return Ok(new APIResponse() { STATUS = APISTATUS.FAIL.ToString(), Response = ex.Message });
            }
        }

        [HttpGet("GetStatuses")]
        public async Task<IActionResult> GetStatuses()
        {
            try
            {
                dynamic expando = new ExpandoObject();
                expando.Products = new TouresHelper().GetStatuses().Select(x => new { ID = x, TEXT = x });
                return Ok(new APIResponse() { STATUS = APISTATUS.PASS.ToString(), Response = expando });
            }
            catch (Exception ex)
            {
                return Ok(new APIResponse() { STATUS = APISTATUS.FAIL.ToString(), Response = ex.Message });
            }
        }

        [HttpGet("GetTourForApproval/{empcode}")]
        public async Task<IActionResult> GetTourForApproval(string empCode)
        {
            try
            {
                var toursList = new TouresHelper().GetTourForApporval(empCode);
                if (toursList.Count > 0)
                {
                    dynamic expando = new ExpandoObject();
                    expando.Tours = toursList;
                    return Ok(new APIResponse() { STATUS = APISTATUS.PASS.ToString(), Response = expando });
                }

                return Ok(new APIResponse() { STATUS = APISTATUS.FAIL.ToString(), Response = "No tours for Approval." });
            }
            catch (Exception ex)
            {
                return Ok(new APIResponse() { STATUS = APISTATUS.FAIL.ToString(), Response = ex.Message });
            }
        }

        [HttpPost("CreateToure")]
        public async Task<IActionResult> CreateToure([FromBody]TourAdvance tourAdvance)
        {
            if (tourAdvance == null)
            {
                return Ok(new APIResponse() { STATUS = APISTATUS.FAIL.ToString(), Response = "Request is empty." });
            }

            try
            {
                string errorMEssage = string.Empty;

                var result = new TouresHelper().InsertTour(tourAdvance, out errorMEssage);
                if (result != null)
                {
                    return Ok(new APIResponse() { STATUS = APISTATUS.PASS.ToString(), Response = result });
                }

                return Ok(new APIResponse() { STATUS = APISTATUS.FAIL.ToString(), Response = errorMEssage });
            }
            catch (Exception ex)
            {
                return Ok(new APIResponse() { STATUS = APISTATUS.FAIL.ToString(), Response =  ex.InnerException == null ? ex.Message : ex.InnerException.Message});
            }
        }

        [HttpGet("GetToures/{empcode}")]
        public async Task<IActionResult> GetToures(string empCode)
        {
            try
            {
                var _visitList = new TouresHelper().GetTours(empCode);
                if (_visitList.Count > 0)
                {
                    dynamic expando = new ExpandoObject();
                    expando.Tours = _visitList;
                    return Ok(new APIResponse() { STATUS = APISTATUS.PASS.ToString(), Response = expando });
                }

                return Ok(new APIResponse() { STATUS = APISTATUS.FAIL.ToString(), Response = "No visits found." });
            }
            catch (Exception ex)
            {
                return Ok(new APIResponse() { STATUS = APISTATUS.FAIL.ToString(), Response = ex.Message });
            }
        }

        [HttpPost("TourApproval/{empCode}")]
        public async Task<IActionResult> TourApproval([FromBody]List<TourAdvance> tourAdvances,string empCode)
        {
            if (tourAdvances.Count == 0)
            {
                return Ok(new APIResponse() { STATUS = APISTATUS.FAIL.ToString(), Response = "Request is empty." });
            }

            try
            {
                string errorMEssage = string.Empty;

                var result = new TouresHelper().TourApprovalProcess(empCode, tourAdvances);
                if (result != null)
                {
                    return Ok(new APIResponse() { STATUS = APISTATUS.PASS.ToString(), Response = result });
                }

                return Ok(new APIResponse() { STATUS = APISTATUS.FAIL.ToString(), Response = errorMEssage });
            }
            catch (Exception ex)
            {
                return Ok(new APIResponse() { STATUS = APISTATUS.FAIL.ToString(), Response = ex.Message });
            }
        }

        [HttpGet("GetArrovedToures/{empcode}")]
        public async Task<IActionResult> GetArrovedToures(string empCode)
        {
            try
            {
                var _visitList = new TouresHelper().GetArrovedToures(empCode);
                if (_visitList.Count > 0)
                {
                    dynamic expando = new ExpandoObject();
                    expando.Tours = _visitList;
                    return Ok(new APIResponse() { STATUS = APISTATUS.PASS.ToString(), Response = expando });
                }

                return Ok(new APIResponse() { STATUS = APISTATUS.FAIL.ToString(), Response = "No visits found." });
            }
            catch (Exception ex)
            {
                return Ok(new APIResponse() { STATUS = APISTATUS.FAIL.ToString(), Response = ex.Message });
            }
        }

        [HttpPost("CreateVisit/{empCode}/{visitType}")]
        public async Task<IActionResult> CreateVisit([FromBody]Visit visit, string empCode,string visitType)
        {
            if (visit == null || string.IsNullOrEmpty(visitType) || string.IsNullOrEmpty(empCode))
            {
                return Ok(new APIResponse() { STATUS = APISTATUS.FAIL.ToString(), Response = "Request is empty." });
            }

            try
            {
                string errorMEssage = string.Empty;

                var result = new TouresHelper().InsertVisit(empCode, visit, visitType);
                if (result != null)
                {
                    CommanHelper.SendMail(iConfig);
                    return Ok(new APIResponse() { STATUS = APISTATUS.PASS.ToString(), Response = result });
                }

                return Ok(new APIResponse() { STATUS = APISTATUS.FAIL.ToString(), Response = errorMEssage });
            }
            catch (Exception ex)
            {
                return Ok(new APIResponse() { STATUS = APISTATUS.FAIL.ToString(), Response = ex.Message });
            }
        }

        [HttpGet("GetVisits/{empcode}")]
        public async Task<IActionResult> GetVisits(string empCode)
        {
            try
            {
                var _visitList = new TouresHelper().GetVisitsOfEmployee(empCode);
                if (_visitList.Count > 0)
                {
                    dynamic expando = new ExpandoObject();
                    expando.Tours = _visitList;
                    return Ok(new APIResponse() { STATUS = APISTATUS.PASS.ToString(), Response = expando });
                }

                return Ok(new APIResponse() { STATUS = APISTATUS.FAIL.ToString(), Response = "No visits found." });
            }
            catch (Exception ex)
            {
                return Ok(new APIResponse() { STATUS = APISTATUS.FAIL.ToString(), Response = ex.Message });
            }
        }

        [HttpGet("GetCLientsList")]
        public async Task<IActionResult> GetCLientsList()
        {
            try
            {
                var _clientList = new TouresHelper().GetClientsList();
                if (_clientList.Count > 0)
                {
                    dynamic expando = new ExpandoObject();
                    expando.Clients = _clientList.Select(x=>new { ID=x.CompanyCode ,TEXT=x.Name});
                    return Ok(new APIResponse() { STATUS = APISTATUS.PASS.ToString(), Response = expando });
                }

                return Ok(new APIResponse() { STATUS = APISTATUS.FAIL.ToString(), Response = "No visits found." });
            }
            catch (Exception ex)
            {
                return Ok(new APIResponse() { STATUS = APISTATUS.FAIL.ToString(), Response = ex.Message });
            }
        }

        [HttpPost("CreateClient")]
        public async Task<IActionResult> CreateClient([FromBody]Companies companies)
        {
            if (companies == null)
            {
                return Ok(new APIResponse() { STATUS = APISTATUS.FAIL.ToString(), Response = "Request is empty." });
            }

            try
            {
                string errorMEssage = string.Empty;

                var result = new TouresHelper().InsertCompanies(companies,out errorMEssage);
                if (result != null)
                {
                    return Ok(new APIResponse() { STATUS = APISTATUS.PASS.ToString(), Response = result });
                }

                return Ok(new APIResponse() { STATUS = APISTATUS.FAIL.ToString(), Response = errorMEssage });
            }
            catch (Exception ex)
            {
                return Ok(new APIResponse() { STATUS = APISTATUS.FAIL.ToString(), Response = ex.Message });
            }
        }

        [HttpGet("GetCLients")]
        public async Task<IActionResult> GetCLients()
        {
            try
            {
               var _clientList = new TouresHelper().GetClientsList();
                if (_clientList.Count > 0)
                {
                    dynamic expando = new ExpandoObject();
                    expando.Clients = _clientList;
                    return Ok(new APIResponse() { STATUS = APISTATUS.PASS.ToString(), Response = expando });
                }

                return Ok(new APIResponse() { STATUS = APISTATUS.FAIL.ToString(), Response = "No visits found." });
            }
            catch (Exception ex)
            {
                return Ok(new APIResponse() { STATUS = APISTATUS.FAIL.ToString(), Response = ex.Message });
            }
        }

        ///Upload bilss
        ///for employee and tour
        ///
        [HttpPost("UploadBills")]
       // public async Task<IActionResult> UploadBills([FromBody]TblTourBills tblTourBills)
        public async Task<IActionResult> UploadBills([FromForm(Name = "BillsImage")]IFormFile Image,[FromForm(Name = "touid")]string toureid, [FromForm(Name = "employeeCode")]string employeeCode)
        {
            if (Image == null)
                return Ok(new APIResponse() { STATUS = APISTATUS.FAIL.ToString(), Response = "Request is empty." });
            if (string.IsNullOrEmpty(employeeCode))
                return Ok(new APIResponse() { STATUS = APISTATUS.FAIL.ToString(), Response = "Request is empty." });
            try
            {
                string dateFile = string.Empty, filePath = string.Empty,fileName=string.Empty;
                var image = Image;

                var uploads = Path.Combine(_webHostEnvironment.WebRootPath, "Images");

                if (image.Length > 0)
                {
                    dateFile = $"{DateTime.Now.Day}{DateTime.Now.Month}{DateTime.Now.Year}{DateTime.Now.Hour}{DateTime.Now.Minute}{DateTime.Now.Second}{DateTime.Now.Millisecond}";
                    fileName = dateFile + image.FileName;

                    filePath = Path.Combine(uploads, fileName);
                   
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(fileStream);
                    }
                }

               
                var tblTourBills = new TblTourBills();
                tblTourBills.Tourid = toureid?.Replace("\"", string.Empty)?.Trim();
                tblTourBills.EmpId = employeeCode?.Replace("\"",string.Empty)?.Trim() ;
                tblTourBills.ImageContent = $"{applicationConfig.Value.ImageFilePath}Images/" + fileName;

                var _tblTourBills = new TouresHelper().AddToureBills(tblTourBills);
                if (_tblTourBills != null)
                {
                    dynamic expando = new ExpandoObject();
                    expando.Bills = _tblTourBills;
                    return Ok(new APIResponse() { STATUS = APISTATUS.PASS.ToString(), Response = expando });
                }

                return Ok(new APIResponse() { STATUS = APISTATUS.FAIL.ToString(), Response = "Failed to upload bills." });
            }
            catch (Exception ex)
            {
                return Ok(new APIResponse() { STATUS = APISTATUS.FAIL.ToString(), Response = ex.InnerException });
            }
        }

        [HttpPost("GetBills")]
        public async Task<IActionResult> GetBills([FromBody]TblTourBills tourBills)
        {
            try
            {
                var _clientList = new TouresHelper().GetToureBills(tourBills?.EmpId, tourBills?.Tourid);
                if (_clientList.Count > 0)
                {
                    dynamic expando = new ExpandoObject();
                    expando.Clients = _clientList;
                    return Ok(new APIResponse() { STATUS = APISTATUS.PASS.ToString(), Response = expando });
                }

                return Ok(new APIResponse() { STATUS = APISTATUS.FAIL.ToString(), Response = "No Bills found." });
            }
            catch (Exception ex)
            {
                return Ok(new APIResponse() { STATUS = APISTATUS.FAIL.ToString(), Response = ex.Message });
            }
        }
    }
}

